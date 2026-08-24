/**
 * Barrameru Outdoor & Camping Store - Exact Store Catalog & LocalStorage Engine
 * Matches template_images items, prices, and categories exactly
 */



const BarrameruStore = {
    _serverWishlistIds: new Set(),
    _wishlistIdsLoaded: false,
    _serverCartItems: [], // [{ id: cartItemId, name, price, image, quantity }]
    _serverCartLoaded: false,

    // Authentication detection from meta tags
    getUserId() {
        try {
            const meta = document.querySelector('meta[name="user-id"]');
            const uid = meta ? meta.getAttribute('content') : '';
            return uid && uid.trim().length > 0 ? uid.trim() : null;
        } catch {
            return null;
        }
    },

    isAuthenticated() {
        return this.getUserId() !== null;
    },
    getAntiForgeryToken() {
        const scoped = document.querySelector(
            '#globalAntiForgeryForm input[name="__RequestVerificationToken"]'
        );
        if (scoped) return scoped.value;
        const any = document.querySelector('input[name="__RequestVerificationToken"]');
        return any ? any.value : '';
    },

    // Guest
    getGuestCartKey() {
        return 'barrameru_cart_guest';
    },
    getGuestCart() {
        try {
            const data = localStorage.getItem(this.getGuestCartKey());
            if (data)
                return JSON.parse(data);
            return [];
        } catch {
            return [];
        }
    },
    saveGuestCart(cart) {
        try {
            localStorage.setItem(this.getGuestCartKey(), JSON.stringify(cart));
            this.updateBadges();
            this.renderMiniCart();
        } catch (e) {
            console.error('Error saving guest cart:', e);
        }
    },
    getGuestWishlistKey() {
        return 'barrameru_wishlist_guest';
    },
    getGuestWishlist() {
        try {
            const data = localStorage.getItem(this.getGuestWishlistKey());
            if (data)
                return JSON.parse(data);
            return [];
        } catch {
            return [];
        }
    },
    saveGuestWishlist(wishlist) {
        try {
            localStorage.setItem(this.getGuestWishlistKey(), JSON.stringify(wishlist));
            this.updateWishlistIcons();
        } catch (e) {
            console.error('Error saving guest wishlist:', e);
        }
    },

    // logged in user
    // getCartKey() {
    //     if (this.isAuthenticated()) {
    //         return this._serverCartItems.map(i => ({
    //             id: i.id,
    //             name: i.name,
    //             category: i.category || 'Outdoor Gear',
    //             price: i.price,
    //             image: i.image,
    //             quantity: i.quantity
    //         }));
    //     }
    //     return this.getGuestCart();
    // },

    // User

    getCart() {
        if (this.isAuthenticated()) {
            return this._serverCartItems.map(i => ({
                id: i.id,
                name: i.name,
                category: i.category || 'Outdoor Gear',
                price: i.price,
                image: i.image,
                quantity: i.quantity
            }));
        }
        return this.getGuestCart(); // check on it later
    },

    async fetchServerCart() {
        if (!this.isAuthenticated()) return;
        try {
            const res = await fetch('/Cart/GetData', {
                headers: { 'Accept': 'application/json' }
            });

           if (res.ok) {
                const data = await res.json();
                this._serverCartItems = (data.cartItems || []).map(ci => ({
                    id: ci.id, // CartItem.Id - needed for UpdateAll / DeleteItem
                    name: ci.productName,
                    price: ci.unitPrice,
                    image: ci.imageUrl || '/images/43.jpg',
                    quantity: ci.quantity
                }));
            } else {
                return;
            }

            this._serverCartLoaded = true;
            this.updateBadges();
            this.renderMiniCart();
        } catch (e) {
            console.error('Error fetching server cart:', e);
        }
    },

    getWishlist() {
        if (this.isAuthenticated()) {
            return Array.from(this._serverWishlistIds).map(id => ({ id }));
        }
        return this.getGuestWishlist();
    },

    isInWishlist(productId) {
        const pId = parseInt(productId);
        if (this.isAuthenticated()) {
            return this._serverWishlistIds.has(pId);
        }
        return this.getGuestWishlist().some(item => item.id === pId);
    },

    async fetchServerWishlistIds() {
        if (!this.isAuthenticated()) return;
        try {
            const res = await fetch('/Wishlist/GetIds', {
                method: 'GET',
                headers: { 'Accept': 'application/json' }
            });
            if (!res.ok) return;
            const data = await res.json();
            if (data.success) {
                this._serverWishlistIds = new Set(data.ids);
                this._wishlistIdsLoaded = true;
                this.updateWishlistIcons();
            }
        } catch (e) {
            console.error('Error fetching wishlist ids:', e);
        }
    },

    async mergeGuestDataOnLogin() {
        if (!this.isAuthenticated()) return;

        try {
            // Merge guest cart
            const guestCart = this.getGuestCart();
            if (guestCart.length > 0) {
                for (const item of guestCart) {
                    try {
                        await fetch('/Cart/AddItem', {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                            body: new URLSearchParams({
                                ProductId: item.id,
                                Quantity: item.quantity || 1,
                                __RequestVerificationToken: this.getAntiForgeryToken()
                            })
                        });
                    } catch (e) {
                        console.error('Error merging guest cart item', item.id, e);
                    }
                }
                localStorage.removeItem(this.getGuestCartKey());
            }

            // Merge guest wishlist
            const guestWishlist = this.getGuestWishlist();
            if (guestWishlist.length > 0) {
                const ids = guestWishlist.map(p => p.id);
                const res = await fetch('/Wishlist/Merge', { // Check on this
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': this.getAntiForgeryToken()
                    },
                    body: JSON.stringify(ids)
                });
                if (res.ok) {
                    localStorage.removeItem(this.getGuestWishlistKey());
                }
            }
        } catch (e) {
            console.error('Error merging guest cart/wishlist on login:', e);
        }
    },


    // Cart mutations

    async addToCart(productId, qty = 1, metadata = null, btnElement = null) {
        const pId = parseInt(productId);
        const quantity = parseInt(qty) || 1;

        if (this.isAuthenticated()) {
            try {
                // AddItem has no [FromBody] server-side, so it binds like a
                // normal form post - send urlencoded, not JSON.
                const res = await fetch('/Cart/AddItem', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    body: new URLSearchParams({
                        ProductId: pId,
                        Quantity: quantity,
                        __RequestVerificationToken: this.getAntiForgeryToken()
                    })
                });

                if (res.status === 401) {
                    window.location.href = '/Account/Login';
                    return;
                }
                if (!res.ok) throw new Error('Server error adding item.');

                await this.fetchServerCart(); // pull authoritative state back down
            } catch (error) {
                console.error('Cart sync error:', error);
                return;
            }
        } else {
            let product = null;

            if (metadata && metadata.name) {
                product = {
                    id: pId,
                    name: metadata.name,
                    category: metadata.category || 'Outdoor Gear',
                    price: parseFloat(metadata.price) || 0,
                    image: metadata.image || metadata.imageUrl || '/images/43.jpg'
                };
            } else if (window.BarrameruCatalog) {
                const found = window.BarrameruCatalog.find(p => p.id === pId);
                if (found) product = { ...found };
            }

            if (!product) {
                product = { id: pId, name: `Gear #${pId}`, category: 'Equipment', price: 45.00, image: '/images/43.jpg' };
            }

            let cart = this.getGuestCart();
            const existingIndex = cart.findIndex(item => item.id === product.id);

            if (existingIndex > -1) {
                cart[existingIndex].quantity += quantity;
            } else {
                cart.push({ ...product, quantity: quantity });
            }

            this.saveGuestCart(cart);
        }

        this.triggerUIFeedback(btnElement);
    },


    async updateCartQuantity(id, newQty) {
        const qty = parseInt(newQty);
        if (qty <= 0) {
            await this.removeFromCart(id);
            return;
        }

        if (this.isAuthenticated()) {
            try {
                const res = await fetch('/Cart/UpdateAll', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': this.getAntiForgeryToken()
                    },
                    body: JSON.stringify([{ Id: parseInt(id), Quantity: qty }])
                });
                if (res.ok) await this.fetchServerCart();
            } catch (e) {
                console.error('Error updating cart quantity:', e);
            }
        } else {
            let cart = this.getGuestCart();
            const item = cart.find(i => i.id === parseInt(id));
            if (item) {
                item.quantity = qty;
                this.saveGuestCart(cart);
            }
        }

        if (typeof window.renderCartPage === 'function') {
            window.renderCartPage();
        }
    },

    async removeFromCart(id) {
        if (this.isAuthenticated()) {
            try {
                const res = await fetch('/Cart/DeleteItem', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    body: new URLSearchParams({
                        id: parseInt(id),
                        __RequestVerificationToken: this.getAntiForgeryToken()
                    })
                });
                if (res.ok) await this.fetchServerCart();
            } catch (e) {
                console.error('Error removing cart item:', e);
            }
        } else {
            let cart = this.getGuestCart();
            cart = cart.filter(i => i.id !== parseInt(id));
            this.saveGuestCart(cart);
        }
        if (typeof window.renderCartPage === 'function') window.renderCartPage();
    },

    async updateAllCartQuantities() {
        const inputs = document.querySelectorAll('.cart-qty-input');
        const updates = Array.from(inputs)
            .map(input => ({
                id: parseInt(input.dataset.id),
                quantity: parseInt(input.value) || 0
            }))
            .filter(u => !isNaN(u.id));

        if (updates.length === 0) return;

        if (this.isAuthenticated()) {
            try {
                const res = await fetch('/Cart/UpdateAll', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': this.getAntiForgeryToken()
                    },
                    body: JSON.stringify(updates.map(u => ({ Id: u.id, Quantity: u.quantity })))
                });
                if (res.ok) await this.fetchServerCart();
            } catch (e) {
                console.error('Error updating cart quantities:', e);
            }
        } else {
            let cart = this.getGuestCart();
            updates.forEach(u => {
                if (u.quantity <= 0) {
                    cart = cart.filter(i => i.id !== u.id);
                } else {
                    const item = cart.find(i => i.id === u.id);
                    if (item) item.quantity = u.quantity;
                }
            });
            this.saveGuestCart(cart);
        }

        if (typeof window.renderCartPage === 'function') {
            window.renderCartPage();
        }

        if (typeof Swal !== 'undefined') {
            Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 1600,
                timerProgressBar: true
            }).fire({ icon: 'success', title: 'Cart updated' });
        }
    },

    // Wishlist mutations

    async toggleWishlist(productId, metadata = null, btnElement = null) {
        const pId = parseInt(productId);

        if (this.isAuthenticated()) {
            try {
                const res = await fetch('/Wishlist/Toggle', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    body: new URLSearchParams({
                        productId: pId,
                        __RequestVerificationToken: this.getAntiForgeryToken()
                    })
                });

                if (res.status === 401) {
                    window.location.href = '/Account/Login';
                    return;
                }

                const data = await res.json();

                if (data.success) {
                    if (data.added) {
                        this._serverWishlistIds.add(pId);
                    } else {
                        this._serverWishlistIds.delete(pId);
                    }

                    this.updateWishlistIcons();
                    this._wishlistFeedback(data.added, metadata?.name || `Item #${pId}`, btnElement);

                    if (typeof window.renderWishlistPage === 'function') {
                        window.renderWishlistPage();
                    }

                    return data.added;
                }
            } catch (e) {
                console.error('Error toggling wishlist:', e);
            }

            return null;
        } else {
            let product = null;

            if (metadata && metadata.name) {
                product = {
                    id: pId,
                    name: metadata.name,
                    category: metadata.category || 'Outdoor Gear',
                    price: parseFloat(metadata.price) || 0,
                    image: metadata.image || metadata.imageUrl || '/images/43.jpg',
                    stock: metadata.stock || 12
                };
            } else if (window.BarrameruCatalog) {
                const found = window.BarrameruCatalog.find(p => p.id === pId);
                if (found) {
                    product = {
                        id: found.id,
                        name: found.name,
                        category: found.category,
                        price: found.price,
                        image: found.image,
                        stock: found.stock || 12
                    };
                }
            }

            if (!product) {
                product = { id: pId, name: `Gear #${pId}`, category: 'Equipment', price: 45.00, image: '/images/43.jpg', stock: 10 };
            }

            let wishlist = this.getGuestWishlist();
            const index = wishlist.findIndex(item => item.id === pId);
            let isAdded = false;

            if (index > -1) {
                wishlist.splice(index, 1);
                isAdded = false;
            } else {
                wishlist.push(product);
                isAdded = true;
            }

            this.saveGuestWishlist(wishlist);
            this._wishlistFeedback(isAdded, product.name, btnElement);

            if (typeof window.renderWishlistPage === 'function') {
                window.renderWishlistPage();
            }

            return isAdded;
        }
    },

    _wishlistFeedback(isAdded, productName, btnElement) {
        if (btnElement) {
            btnElement.classList.add('heart-pop-anim');
            setTimeout(() => btnElement.classList.remove('heart-pop-anim'), 400);
        }

        if (typeof Swal !== 'undefined') {
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 1800,
                timerProgressBar: true
            });
            Toast.fire({
                icon: isAdded ? 'success' : 'info',
                title: isAdded ? `Saved "${productName}" to Wishlist ❤️` : `Removed from Wishlist`
            });
        }
    },

    // Derived data / rendering

    getTotals() {
        const cart = this.getCart();
        const subtotal = cart.reduce((sum, item) => sum + ((parseFloat(item.price) || 0) * (parseInt(item.quantity) || 1)), 0);
        const count = cart.reduce((sum, item) => sum + (parseInt(item.quantity) || 1), 0);

        return {
            count: count,
            subtotal: subtotal.toFixed(2),
            total: subtotal.toFixed(2)
        };
    },

    updateBadges() {
        const totals = this.getTotals();
        const cartBadges = document.querySelectorAll('.header-cart-badge, .cart-badge-count');
        cartBadges.forEach(b => b.textContent = totals.count);
    },

    updateWishlistIcons() {
        document.querySelectorAll('.btn-card-wishlist, .btn-wishlist-toggle, .template-wishlist-btn').forEach(btn => {
            const pId = parseInt(btn.getAttribute('data-product-id'));
            if (!isNaN(pId)) {
                const inWishlist = this.isInWishlist(pId);
                const icon = btn.querySelector('i');
                if (inWishlist) {
                    btn.classList.add('in-wishlist');
                    btn.setAttribute('title', 'Remove from Wishlist');
                    if (icon) icon.className = 'bi bi-heart-fill text-danger';
                } else {
                    btn.classList.remove('in-wishlist');
                    btn.setAttribute('title', 'Save to Wishlist');
                    if (icon) icon.className = 'bi bi-heart';
                }
            }
        });

        const wishlistBadges = document.querySelectorAll('.wishlist-badge-count');
        const count = this.isAuthenticated() ? this._serverWishlistIds.size : this.getGuestWishlist().length;
        wishlistBadges.forEach(b => b.textContent = count);
    },

    renderMiniCart() {
        const container = document.getElementById('miniCartItemsContainer');
        const emptyMsg = document.getElementById('miniCartEmpty');
        const totalElem = document.getElementById('miniCartSubtotal');
        if (!container) return;

        const cart = this.getCart();
        const totals = this.getTotals();

        if (cart.length === 0) {
            container.innerHTML = '';
            if (emptyMsg) emptyMsg.style.display = 'block';
            if (totalElem) totalElem.textContent = '$0.00';
            return;
        }

        if (emptyMsg) emptyMsg.style.display = 'none';
        if (totalElem) totalElem.textContent = `$${totals.subtotal}`;

        container.innerHTML = cart.map(item => `
      <div class="mini-cart-item d-flex align-items-center gap-3 py-3 border-bottom">
        <img src="${item.image}" alt="${item.name}" style="width: 52px; height: 52px; object-fit: contain; background: #fff;" class="border flex-shrink-0" onerror="this.src='/images/placeholder.png';">
        <div class="flex-grow-1 min-w-0" style="min-width: 0;">
          <div class="d-flex justify-content-between align-items-start gap-1">
            <div class="small fw-bold text-secondary text-truncate" title="${item.name}">${item.name}</div>
            <button class="btn btn-sm text-danger border-0 p-0 flex-shrink-0" onclick="BarrameruStore.removeFromCart(${item.id})" title="Remove item">
              <i class="bi bi-x fs-5"></i>
            </button>
          </div>
          <div class="small text-muted mb-1">$${(parseFloat(item.price) || 0).toFixed(2)} each</div>
          <div class="d-flex justify-content-between align-items-center mt-1">
            <div class="mini-cart-qty-ctrl d-inline-flex align-items-center">
              <button type="button" class="mini-cart-qty-btn" onclick="BarrameruStore.updateCartQuantity(${item.id}, ${(item.quantity || 1) - 1})" title="Decrease quantity">
                <i class="bi bi-dash"></i>
              </button>
              <span class="mini-cart-qty-val">${item.quantity || 1}</span>
              <button type="button" class="mini-cart-qty-btn" onclick="BarrameruStore.updateCartQuantity(${item.id}, ${(item.quantity || 1) + 1})" title="Increase quantity">
                <i class="bi bi-plus"></i>
              </button>
            </div>
            <strong class="small text-dark">$${((parseFloat(item.price) || 0) * (item.quantity || 1)).toFixed(2)}</strong>
          </div>
        </div>
      </div>
    `).join('');
    },

    triggerUIFeedback(btnElement) {
        if (btnElement) {
            btnElement.disabled = true;
            const origHtml = btnElement.innerHTML;
            btnElement.innerHTML = '<i class="bi bi-check2"></i> Added!';
            btnElement.classList.add('btn-added-success');
            setTimeout(() => {
                btnElement.innerHTML = origHtml;
                btnElement.disabled = false;
                btnElement.classList.remove('btn-added-success');
            }, 1400);
        }

        if (typeof Swal !== 'undefined') {
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 1500,
                timerProgressBar: true,
                iconColor: '#B67961'
            });
            Toast.fire({ icon: 'success', title: 'Added to cart successfully!' });
        }
    },

    async init() {
        await this.mergeGuestDataOnLogin();

        if (this.isAuthenticated()) {
            await this.fetchServerWishlistIds();
            await this.fetchServerCart();
        } else {
            const guestCart = this.getGuestCart();
            //DEMO
            // if (!localStorage.getItem(this.getGuestCartKey()) && guestCart.length === 0) {
            //     this.saveGuestCart([
            //         { id: 7, name: 'Black Binoculars', category: 'Knives & Tools', price: 65.00, image: '/images/high-angle-view-of-confident-couple-climbing-mountain-e1664201089286.jpg', quantity: 1 },
            //         { id: 2, name: 'Yellow Tent', category: 'Tent & Accesories', price: 65.00, image: '/images/orange-tourist-tent-illuminated-from-inside-stands-in-mountains-above-clouds.jpg', quantity: 1 },
            //         { id: 14, name: 'Camouflage Backpack', category: 'Bags & Pack', price: 140.00, image: '/images/group-of-friends-with-backpacks-doing-trekking-excursion-on-mountain.jpg', quantity: 1 }
            //     ]);
            // }
        }

        this.updateBadges();
        this.updateWishlistIcons();
        this.renderMiniCart();
    }
};

document.addEventListener('DOMContentLoaded', () => {
    BarrameruStore.init();
});
