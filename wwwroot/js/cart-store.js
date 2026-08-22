/**
 * Barrameru Outdoor & Camping Store - Exact Store Catalog & LocalStorage Engine
 * Matches template_images items, prices, and categories exactly
 */


const offcanvasCart = document.getElementById('offcanvasCart');

const BarrameruStore = {
  async getCart() {
    
        try {
            const url = `/Cart/GetData`;
            const response = await fetch(url);
            if (!response.ok)
                throw new Error(`HTTP error! Status: ${response.status}`);

            const data = await response.json();
            return data ? data.cartItems : [];
        }
        catch (error) {
            console.error("Failed to fetch entry:", error);
            return null;
        }
  },

  saveCart(cart) {
    localStorage.setItem('barrameru_cart', JSON.stringify(cart));
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

  addToCart(productId, qty = 1) {
    const product = window.BarrameruCatalog.find(p => p.id === parseInt(productId));
    if (!product) return;

    const cart = this.getCart();
    const existingIndex = cart.findIndex(item => item.id === product.id);

    if (existingIndex > -1) {
      cart[existingIndex].quantity += parseInt(qty);
    } else {
      cart.push({
        id: product.id,
        name: product.name,
        category: product.category,
        price: product.price,
        image: product.image,
        quantity: parseInt(qty)
      });
    }

    this.saveCart(cart);

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

  async updateCartQuantity(cartItemId, newQty) {
    let cart = await this.getCart();
    const qty = parseInt(newQty);
    if (qty <= 0) {
        await this.removeFromCart(cartItemId);
      return;
      }

      try {
          const url = `/Cart/Update`;
          const response = await fetch(url, {
              method: 'POST',
              headers: {
                  'Content-Type': 'application/json',
              },
              body: JSON.stringify({ cartItemId: cartItemId, quantity: qty })
          });

          if (!response.ok)
              throw new Error(`HTTP error! Status: ${response.status}`);
          }
      catch (error) {
          console.error("Failed to update data:", error);
      }
  },

  async removeFromCart(itemId) {
      try {
          const url = `/Cart/DeleteItem/${itemId}`;
          const response = await fetch(url);
          if (!response.ok)
              throw new Error(`HTTP error! Status: ${response.status}`);
          this.renderMiniCart();
      }
      catch (error) {
          console.error("Failed to delete entry:", error);
      }
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
    cartBadges.forEach(b => b.textContent = length);
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
      
    if (cart.length === 0 || cart === null) {
        container.innerHTML = '';
        emptyMsg.style.display = 'block';
        if (cart === null)
            emptyMsg.querySelector("h6").textContent = "Connection failed";
        totalElem.textContent = '$0.00';
        return;
    }

    if (emptyMsg) emptyMsg.style.display = 'none';

    const totals = this.getTotals(cart);
    if (totalElem) totalElem.textContent = `$${totals.subtotal}`;

      this.updateBadges(cart.length);

    container.innerHTML = cart.map(item => `
      <div class="d-flex align-items-center gap-3 py-2 border-bottom">
        <img src="${item.imageUrl}" alt="${item.productName}" style="width: 50px; height: 50px; object-fit: contain; background: #fff;" class="border">
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
