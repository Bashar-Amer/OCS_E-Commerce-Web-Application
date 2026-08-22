
const offcanvasCart = document.getElementById('offcanvasCart');

const BarrameruStore = {
    isLoggedIn: typeof window.isUserAuthenticated !== 'undefined' ? window.isUserAuthenticated : false,

    async getCart() {
        if (this.isLoggedIn) {
            try {
                const response = await fetch(`/Cart/GetData`);
                if (response.ok) {
                    const data = await response.json();
                    return data ? data.cartItems : [];
                }
            } catch (error) {
                console.error("Failed to fetch server cart, falling back to local storage:", error);
            }
        }

        try {
            const data = localStorage.getItem('barrameru_cart');
            return data ? JSON.parse(data) : [];
        } catch {
            return [];
        }
    },

    async saveCart(cart) {
        if (this.isLoggedIn) {
            // If logged in, sync changes to your server database via API/Controller actions
            // (Assuming you have backend endpoints or handlers for this)
        } else {
            // If guest, save locally
            localStorage.setItem('barrameru_cart', JSON.stringify(cart));
        }
        this.updateBadges();
        this.renderMiniCart();
    },

  getWishlist() {
    try {
      const data = localStorage.getItem('barrameru_wishlist');
      return data ? JSON.parse(data) : [];
    } catch {
      return [];
    }
  },

  saveWishlist(wishlist) {
    localStorage.setItem('barrameru_wishlist', JSON.stringify(wishlist));
    this.updateBadges();
  },

    async addToCart(productId, qty = 1) {
        const product = window.BarrameruCatalog.find(p => p.id === parseInt(productId));
        if (!product) return;

        if (this.isLoggedIn) {
            //Call your backend server action to add to user's database cart
            try {
                await fetch(`/Cart/AddItem`, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ productId: product.id, quantity: parseInt(qty) })
                });
            } catch (error) {
                console.error("Failed to add to server cart:", error);
            }
        } else {
            //Handle local storage cart modification for guests
            let cart = await this.getCart();
            const existingIndex = cart.findIndex(item => item.id === product.id);

            if (existingIndex > -1) {
                cart[existingIndex].quantity += parseInt(qty);
            } else {
                cart.push({
                    id: product.id,
                    productName: product.name,
                    category: product.category,
                    unitPrice: product.price,
                    imageUrl: product.image,
                    quantity: parseInt(qty)
                });
            }
            localStorage.setItem('barrameru_cart', JSON.stringify(cart));
        }

        this.updateBadges();
        this.renderMiniCart();

        if (typeof Swal !== 'undefined') {
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 2500,
                timerProgressBar: true,
                iconColor: '#B67961'
            });
            Toast.fire({
                icon: 'success',
                title: `Added "${product.name}" to cart!`
            });
        }
    },

    async updateAllCartQuantities() {
        const inputs = document.querySelectorAll('.cart-qty-input');
        const updates = [];

        const currentCart = await this.getCart();
        let hasChanges = false;

        inputs.forEach(input => {
            const id = parseInt(input.getAttribute('data-id'));
            const quantity = parseInt(input.value);

            if (quantity > 0) {
                updates.push({ id, quantity });

                const cartItem = currentCart.find(i => i.id === id);
                if (cartItem && cartItem.quantity !== quantity) {
                    hasChanges = true;
                }
            }
        });

        if (!hasChanges) {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    toast: true,
                    position: 'top-end',
                    icon: 'info',
                    title: 'No changes to update.',
                    showConfirmButton: false,
                    timer: 1500
                });
            }
            return;
        }

        inputs.forEach(input => {
            const id = parseInt(input.getAttribute('data-id'));
            const quantity = parseInt(input.value);
            if (quantity > 0) {
                updates.push({ id, quantity });
            }
        });

        if (this.isLoggedIn) {
            // Send bulk updates to server (adjust endpoint based on your backend API)
            try {
                const response = await fetch('/Cart/UpdateAll', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(updates)
                });
                if (!response.ok) throw new Error('Failed to update cart');
            } catch (error) {
                console.error("Error updating cart quantities:", error);
            }
        } else {
            // Update local storage cart
            let cart = await this.getCart();
            updates.forEach(update => {
                const item = cart.find(i => i.id === update.id);
                if (item) {
                    item.quantity = update.quantity;
                }
            });
            localStorage.setItem('barrameru_cart', JSON.stringify(cart));
        }

        // Refresh mini cart, badges, and the full cart page
        this.renderMiniCart();
        if (typeof window.renderCartPage === 'function') {
            window.renderCartPage();
        }

        if (typeof Swal !== 'undefined') {
            Swal.fire({
                toast: true,
                position: 'top-end',
                icon: 'success',
                title: 'Cart updated successfully!',
                showConfirmButton: false,
                timer: 2000
            });
        }
    },

    async removeFromCart(itemId) {
        if (this.isLoggedIn) {
            try {
                await fetch(`/Cart/DeleteItem/${itemId}`);
            } catch (error) {
                console.error("Failed to delete server entry:", error);
            }
        } else {
            let cart = await this.getCart();
            cart = cart.filter(item => item.id !== itemId);
            localStorage.setItem('barrameru_cart', JSON.stringify(cart));
        }
        this.renderMiniCart();
    },

  toggleWishlist(productId) {
    const product = window.BarrameruCatalog.find(p => p.id === parseInt(productId));
    if (!product) return;

    let wishlist = this.getWishlist();
    const index = wishlist.findIndex(item => item.id === product.id);

    if (index > -1) {
      wishlist.splice(index, 1);
      this.saveWishlist(wishlist);
      if (typeof Swal !== 'undefined') {
        Swal.fire({
          toast: true,
          position: 'top-end',
          icon: 'info',
          title: `Removed from Wishlist`,
          showConfirmButton: false,
          timer: 2000
        });
      }
    } else {
      wishlist.push({
        id: product.id,
        name: product.name,
        category: product.category,
        price: product.price,
        image: product.image,
        stock: product.stock
      });
      this.saveWishlist(wishlist);
      if (typeof Swal !== 'undefined') {
        Swal.fire({
          toast: true,
          position: 'top-end',
          icon: 'success',
          title: `Saved to Wishlist ❤️`,
          showConfirmButton: false,
          timer: 2000
        });
      }
    }
  },

  getTotals(cart) {
    const subtotal = cart.reduce((sum, item) => sum + (item.unitPrice * item.quantity), 0);
    const count = cart.reduce((sum, item) => sum + item.quantity, 0);

    return {
      count: count,
      subtotal: subtotal.toFixed(2),
      total: subtotal.toFixed(2)
    };
  },

  updateBadges(length) {
    const cartBadges = document.querySelectorAll('.header-cart-badge, .cart-badge-count');
    cartBadges.forEach(b => b.textContent = length ?? 0);
  },

  async renderMiniCart() {
      const container = offcanvasCart.querySelector('#miniCartItemsContainer');
      const emptyMsg = offcanvasCart.querySelector('#miniCartEmpty');
      const totalElem = offcanvasCart.querySelector('#miniCartSubtotal');
      if (!container) return;

      container.innerHTML = `
        <div class="text-center py-5">
            <div class="spinner-border text-teal" role="status" style="color: #5E959F;">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
    `;

    const cart = await this.getCart();
      
      if (cart === null || cart.length === 0 ) {
        container.innerHTML = '';
        emptyMsg.style.display = 'block';
        if (cart === null)
            emptyMsg.querySelector("h6").textContent = "Connection failed";
          totalElem.textContent = '$0.00';
          this.updateBadges(0);
        return;
    }

    if (emptyMsg) emptyMsg.style.display = 'none';

    const totals = this.getTotals(cart);
    if (totalElem) totalElem.textContent = `$${totals.subtotal}`;

      this.updateBadges(cart.length);

    container.innerHTML = cart.map(item => `
      <div class="d-flex align-items-center gap-3 py-2 border-bottom">
        <img src="${item.imageUrl}" alt="${item.productName}" style="width: 50px; height: 50px; object-fit: contain; background: #fff;" class="border"
        onerror="this.onerror=null; this.src='/images/placeholder.png'">
        <div class="flex-grow-1">
          <div class="small fw-bold text-secondary text-truncate" style="max-width: 180px;">${item.productName}</div>
          <div class="small text-muted">${item.quantity} × $${item.unitPrice.toFixed(2)}</div>
        </div>
        <button class="btn btn-sm text-danger border-0" onclick="BarrameruStore.removeFromCart(${item.id})">
          <i class="bi bi-x fs-5"></i>
        </button>
      </div>
    `).join('');

  },

};

document.addEventListener('DOMContentLoaded', () => {
    BarrameruStore.renderMiniCart();
});


// THIS IS A CODE TO TRY LOCAL STORAGE DATA

localStorage.setItem('barrameru_cart', JSON.stringify([
    {
        "id": 1,
        "productName": "Wireless Bluetooth Headphones",
        "imageUrl": "/images/top-view-of-travel-equipment-for-a-mountain-trip-e1664201262219.jpg",
        "unitPrice": 49.99,
        "quantity": 2
    },
    {
        "id": 2,
        "productName": "Orange Tourist Tent",
        "imageUrl": "/images/orange-tourist-tent-illuminated-from-inside-stands-in-mountains-above-clouds.jpg",
        "unitPrice": 120.00,
        "quantity": 1
    },
    {
        "id": 3,
        "productName": "Ergonomic Office Chair",
        "imageUrl": "",
        "unitPrice": 150.50,
        "quantity": 1
    }
]));

