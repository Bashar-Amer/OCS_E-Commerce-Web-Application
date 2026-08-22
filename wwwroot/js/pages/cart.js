/**
 * PAGE SCRIPT: CART (Index.cshtml)
 */

window.renderCartPage = async function () {

    const tbody = document.getElementById('cartTableBodyExact');
    tbody.innerHTML = `
        <div class="text-center py-5">
            <div class="spinner-border text-teal" role="status" style="color: #5E959F;">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>`; 

    const cart = await BarrameruStore.getCart();
    const totals = BarrameruStore.getTotals(cart);
    
    if (!tbody) return;

    if (cart.length === 0) {
        tbody.innerHTML = '<tr><td colspan="5" class="text-center py-4 text-muted">Your cart is empty. <a href="/Shop" class="text-primary fw-bold">Continue shopping</a></td></tr>';
        const subtotal = document.getElementById('cartSubtotalVal');
        const total = document.getElementById('cartTotalVal');
        if (subtotal) subtotal.textContent = '$0.00';
        if (total) total.textContent = '$0.00';
        return;
    }

    tbody.innerHTML = cart.map(item => `
        <tr class="border-bottom">
            <td>
                <button class="btn btn-sm text-teal border-0 p-0" style="color: #5E959F;"
                onclick="BarrameruStore.removeFromCart(${item.id}); window.renderCartPage();">
                    <i class="bi bi-x fs-4"></i>
                </button>
            </td>
            <td>
                <div class="d-flex align-items-center gap-3">
                    <img src="${item.imageUrl}" alt="${item.productName}" style="width: 44px; height: 44px; object-fit: contain;">
                    <span class="cart-item-title">${item.productName}</span>
                </div>
            </td>
            <td class="small text-secondary">$${item.unitPrice.toFixed(2)}</td>
            <td>
                <input type="number" class="form-control form-control-sm text-center rounded-0" style="width: 60px;" value="${item.quantity}" min="1" onchange="BarrameruStore.updateCartQuantity(${item.id}, this.value)">
            </td>
            <td class="small fw-bold text-secondary">$${(item.unitPrice * item.quantity).toFixed(2)}</td>
        </tr>
    `).join('');

    const subtotal = document.getElementById('cartSubtotalVal');
    const total = document.getElementById('cartTotalVal');
    if (subtotal) subtotal.textContent = `$${totals.subtotal}`;
    if (total) total.textContent = `$${totals.total}`;
};

document.addEventListener('DOMContentLoaded', () => {
    window.renderCartPage();

    const btnCoupon = document.getElementById('btnCartApplyCoupon');
    if (btnCoupon) {
        btnCoupon.addEventListener('click', () => {
            const input = document.getElementById('cartCouponInput');
            const val = input ? input.value.trim() : '';
            if (val) {
                Swal.fire({
                    icon: 'success',
                    title: 'Coupon Applied!',
                    text: `Coupon code "${val}" has been activated.`,
                    confirmButtonColor: '#B67961'
                });
            }
        });
    }
});
