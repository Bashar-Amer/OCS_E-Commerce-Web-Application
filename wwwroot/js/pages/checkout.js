/**
 * PAGE SCRIPT: CHECKOUT (Index.cshtml)
 */


document.addEventListener('DOMContentLoaded', async () => {

    const container = document.getElementById('checkoutOrderItemsList');
    const chkSubtotal = document.getElementById('chkSubtotal');
    const chkTotal = document.getElementById('chkTotal');

    if (BarrameruStore.isAuthenticated()) {
        await BarrameruStore.fetchServerCart();
    }

    const cart = BarrameruStore.getCart();
    const totals = BarrameruStore.getTotals();

    if (container) {
        if (cart.length ===  0) {
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

    if (chkSubtotal) chkSubtotal.textContent = `$${totals.subtotal}`;
    if (chkTotal) chkTotal.textContent = `$${totals.total}`;

    const form = document.getElementById('exactCheckoutForm');
    const submitBtn = form ? form.querySelector('button[type="submit"]') : null;

    if (form && submitBtn) {
        form.addEventListener('submit', async (e) => {
            e.preventDefault();

            if (cart.length === 0) {
                e.preventDefault(); 
                return;
            }

            submitBtn.disabled = true;
            submitBtn.textContent = 'Placing order...';

            const formData = new FormData(form);
            try {
                const response = await fetch('/Order/Checkout', {
                    method: 'POST',
                    body: formData,
                    headers: {
                        'X-Requested-With': 'XMLHttpRequest'
                    }
                });
                if (response.redirected) {
                    window.location.href = response.url;
                    return;
                }
                const contentType = response.headers.get("content-type");

                if (contentType && contentType.includes("text/html")) {
                    const html = await response.text();
                    document.open();
                    document.write(html);
                    document.close();
                    return;
                }
                if (!response.ok) {
                    console.log(response);
                    throw new Error('Failed to place order. Please try again.');
                }
                else {
                    const data = await response.json();
                    if (data.success && data.orderId) {
                        if (typeof Swal !== 'undefined') {
                            Swal.fire({
                                icon: 'success',
                                title: 'Order Placed Successfully!',
                                text: `Thank you for your order #${data.orderId}. Redirecting to your invoice...`,
                                confirmButtonColor: '#B67961',
                                timer: 2000,
                                showConfirmButton: false
                            }).then(() => {
                                window.location.href = `/Order/Invoice?id=${data.orderId}`;
                            });
                        } else window.location.href = `/Order/Invoice?id=${data.orderId}`;
                    }
                    else throw new Error('Invalid response from server.');
                }
            } catch (error) {
                console.error("Checkout Error:", error);
                alert("An error occurred while processing your order. Please review your cart and try again.");
                submitBtn.disabled = false;
                submitBtn.textContent = 'PLACE ORDER';
            }
        });
    }
});




