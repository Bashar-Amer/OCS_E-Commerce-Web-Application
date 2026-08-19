/**
 * PAGE SCRIPT: INVOICE (Invoice.cshtml)
 */

document.addEventListener('DOMContentLoaded', () => {
    const raw = sessionStorage.getItem('barrameru_last_order');
    if (raw) {
        try {
            const order = JSON.parse(raw);
            const numElem = document.getElementById('invOrderNumber');
            if (numElem) numElem.textContent = '#' + order.orderId;

            const dateElem = document.getElementById('invOrderDate');
            if (dateElem) dateElem.textContent = order.date;

            const nameElem = document.getElementById('invCustomerName');
            if (nameElem && order.customer) nameElem.textContent = order.customer.name;

            const emailElem = document.getElementById('invCustomerEmail');
            if (emailElem && order.customer) emailElem.textContent = order.customer.email;

            const addrElem = document.getElementById('invCustomerAddress');
            if (addrElem && order.customer) addrElem.textContent = order.customer.address;

            const tbody = document.getElementById('invoiceItemsBody');
            if (tbody && order.items && order.items.length > 0) {
                tbody.innerHTML = order.items.map(item => `
                    <tr>
                        <td>
                            <strong>${item.name}</strong>
                            <div class="small text-muted">${item.category}</div>
                        </td>
                        <td class="text-center">$${item.price.toFixed(2)}</td>
                        <td class="text-center">${item.quantity}</td>
                        <td class="text-end fw-bold">$${(item.price * item.quantity).toFixed(2)}</td>
                    </tr>
                `).join('');
            }

            if (order.totals) {
                const sub = document.getElementById('invSubtotal');
                const tot = document.getElementById('invTotal');
                if (sub) sub.textContent = `$${order.totals.subtotal}`;
                if (tot) tot.textContent = `$${order.totals.total}`;
            }
        } catch (e) {
            console.error(e);
        }
    }
});
