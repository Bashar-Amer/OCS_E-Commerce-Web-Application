/**
 * PAGE SCRIPT: CHECKOUT (Index.cshtml)
 */


function renderCartIssues(issues) {
    const container = document.getElementById('cartIssuesContainer');
    if (!container) return;

    container.innerHTML = `
        <div class="alert alert-warning alert-dismissible fade show mb-4 rounded-0" role="alert">
            <strong>Notice regarding your cart:</strong>
            <ul class="mb-0 ps-3">
                ${issues.map(issue => `<li>${issue}</li>`).join('')}
            </ul>
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;

    container.scrollIntoView({ behavior: 'smooth', block: 'start' });
}

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

                const contentType = response.headers.get("content-type");
                const data = contentType && contentType.includes("application/json")
                    ? await response.json()
                    : null;

                if (!response.ok || !data || !data.success) {
                    const issues = (data && data.issues) ? data.issues : ['Failed to place order. Please try again.'];
                    renderCartIssues(issues);
                    throw new Error(issues.join(' '));
                }
                
                if (data.orderId) {
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
                
            } catch (error) {
                console.error("Checkout Error:", error);
                // alert("An error occurred while processing your order. Please review your cart and try again.");
                submitBtn.disabled = false;
                submitBtn.textContent = 'PLACE ORDER';
            }
        });
    }
});




