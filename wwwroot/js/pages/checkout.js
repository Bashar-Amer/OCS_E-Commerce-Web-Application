/**
 * PAGE SCRIPT: CHECKOUT (Index.cshtml)
 */

document.addEventListener('DOMContentLoaded', () => {
    const cart = BarrameruStore.getCart();
    const totals = BarrameruStore.getTotals();
    const container = document.getElementById('checkoutOrderItemsList');

    if (container) {
        if (cart.length === 0) {
            container.innerHTML = '<div class="py-3 text-muted small">No items in order. <a href="/Shop">Go to Shop</a></div>';
        } else {
            container.innerHTML = cart.map(item => `
                <div class="d-flex justify-content-between py-2 border-bottom small text-secondary">
                    <span>${item.name} × ${item.quantity}</span>
                    <span>$${(item.price * item.quantity).toFixed(2)}</span>
                </div>
            `).join('');
        }
    }

    const chkSubtotal = document.getElementById('chkSubtotal');
    const chkTotal = document.getElementById('chkTotal');
    if (chkSubtotal) chkSubtotal.textContent = `$${totals.subtotal}`;
    if (chkTotal) chkTotal.textContent = `$${totals.total}`;

    const form = document.getElementById('exactCheckoutForm');
    if (form) {
        form.addEventListener('submit', (e) => {
            e.preventDefault();
            const orderId = 'ORD-' + Math.floor(100000 + Math.random() * 900000);

            const orderObj = {
                orderId: orderId,
                date: new Date().toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' }),
                customer: {
                    name: (document.getElementById('chkFirstName')?.value || '') + ' ' + (document.getElementById('chkLastName')?.value || ''),
                    email: document.getElementById('chkEmail')?.value || '',
                    phone: document.getElementById('chkPhone')?.value || '',
                    address: (document.getElementById('chkStreet')?.value || '') + ', ' + (document.getElementById('chkCity')?.value || '')
                },
                items: [...cart],
                totals: { ...totals },
                status: 'Processing',
                paymentStatus: 'Approved'
            };
            sessionStorage.setItem('barrameru_last_order', JSON.stringify(orderObj));

            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    icon: 'success',
                    title: 'Order Placed Successfully!',
                    text: `Thank you for your order #${orderId}. Redirecting to your invoice...`,
                    confirmButtonColor: '#B67961'
                }).then(() => {
                    BarrameruStore.saveCart([]);
                    window.location.href = `/Order/Invoice?id=${orderId}`;
                });
            } else {
                BarrameruStore.saveCart([]);
                window.location.href = `/Order/Invoice?id=${orderId}`;
            }
        });
    }
});
