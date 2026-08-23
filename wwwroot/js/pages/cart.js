/**
 * PAGE SCRIPT: CART (Index.cshtml)
 */


function escapeHtml(str) {
    return String(str ?? '').replace(/[&<>"']/g, c => ({
        '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;'
    }[c]));
}

function money(n) {
    return `$${(parseFloat(n) || 0).toFixed(2)}`;
}


function renderLoading(tbody) {
    tbody.innerHTML = `
            <tr>
                <td colspan="5" class="text-center py-5">
                    <div class="spinner-border text-teal" role="status" style="color: #5E959F;">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                </td>
            </tr>`;
}

function renderEmpty(tbody) {
    tbody.innerHTML = '<tr><td colspan="5" class="text-center py-4 text-muted">Your cart is empty. <a href="/Shop" class="text-primary fw-bold">Continue shopping</a></td></tr>';
}

function renderRows(tbody, cart) {
    tbody.innerHTML = cart.map(item => `
            <tr class="border-bottom">
                <td>
                    <button type="button" class="btn btn-sm text-teal border-0 p-0 btn-remove-cart-item" style="color: #5E959F;" data-id="${item.id}">
                        <i class="bi bi-x fs-4"></i>
                    </button>
                </td>
                <td>
                    <div class="d-flex align-items-center gap-3">
                        <img src="${item.image}" alt="${escapeHtml(item.name)}" style="width: 44px; height: 44px; object-fit: contain;"
                             onerror="this.onerror=null; this.src='/images/placeholder.png'">
                        <span class="cart-item-title">${escapeHtml(item.name)}</span>
                    </div>
                </td>
                <td class="small text-secondary">${money(item.price)}</td>
                <td>
                    <input type="number" class="form-control form-control-sm text-center rounded-0 cart-qty-input"
                           style="width: 60px;" data-id="${item.id}" value="${item.quantity}" min="1">
                </td>
                <td class="small fw-bold text-secondary">${money(item.price * item.quantity)}</td>
            </tr>
        `).join('');
}

function updateSummary(totals) {
    const subtotal = document.getElementById('cartSubtotalVal');
    const total = document.getElementById('cartTotalVal');
    if (subtotal) subtotal.textContent = money(totals.subtotal);
    if (total) total.textContent = money(totals.total);
}

window.renderCartPage = async function () {
    const tbody = document.getElementById('cartTableBodyExact');
    if (!tbody) return;

    renderLoading(tbody);

    if (BarrameruStore.isAuthenticated()) {
        await BarrameruStore.fetchServerCart();
    }

    const cart = BarrameruStore.getCart();

    // console.log(cart);

    if (cart.length === 0) {
        renderEmpty(tbody);
        updateSummary({ subtotal: 0, total: 0 });
        return;
    }

    renderRows(tbody, cart);
    updateSummary(BarrameruStore.getTotals());
};


async function handleUpdateCartClick(btn) {
    btn.disabled = true;
    try {
        await BarrameruStore.updateAllCartQuantities();
    } finally {
        btn.disabled = false;
    }
}


document.addEventListener('DOMContentLoaded', () => {
    window.renderCartPage();

    const tbody = document.getElementById('cartTableBodyExact');
    if (tbody) {
        tbody.addEventListener('click', (e) => {
            const btn = e.target.closest('.btn-remove-cart-item');
            if (!btn) return;
            const id = parseInt(btn.dataset.id);
            if (!isNaN(id)) BarrameruStore.removeFromCart(id);
        });
    }

    const btnUpdate = document.getElementById('btnUpdateCart');
    if (btnUpdate) {
        btnUpdate.addEventListener('click', () => handleUpdateCartClick(btnUpdate));
    }

    const btnCoupon = document.getElementById('btnCartApplyCoupon');
    if (btnCoupon) {
        btnCoupon.addEventListener('click', () => {
            const input = document.getElementById('cartCouponInput');
            const val = input ? input.value.trim() : '';
            if (val) {
                Swal.fire({
                    icon: 'success',
                    title: 'Coupon Applied!',
                    text: `Coupon code "${escapeHtml(val)}" has been activated.`,
                    confirmButtonColor: '#B67961'
                });
            }
        });
    }
});