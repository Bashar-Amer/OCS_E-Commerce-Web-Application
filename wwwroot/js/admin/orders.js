/**
 * BARRAMERU ADMIN: ORDERS SCRIPT (orders.js)
 * Live table search, status filter, and SweetAlert2 status confirmation
 */

document.addEventListener('DOMContentLoaded', () => {
    // 1. Table Live Filtering
    const searchInput = document.getElementById('orderSearchInput');
    const tableBody = document.getElementById('ordersTableBody');
    const statusSelect = document.getElementById('orderStatusFilter');

    function filterRows() {
        const query = searchInput?.value.toLowerCase().trim() || '';
        const selectedStatus = statusSelect?.value || '';
        const rows = tableBody?.querySelectorAll('tr.order-row') || [];

        let visibleCount = 0;
        rows.forEach(row => {
            const idText = row.querySelector('.order-id-cell')?.textContent.toLowerCase() || '';
            const customerText = row.querySelector('.order-customer-cell')?.textContent.toLowerCase() || '';
            const rowStatus = row.getAttribute('data-status') || '';

            const matchesQuery = idText.includes(query) || customerText.includes(query);
            const matchesStatus = selectedStatus === '' || rowStatus.toLowerCase() === selectedStatus.toLowerCase();

            if (matchesQuery && matchesStatus) {
                row.style.display = '';
                visibleCount++;
            } else {
                row.style.display = 'none';
            }
        });

        const emptyRow = document.getElementById('noOrderMatchRow');
        if (emptyRow) {
            emptyRow.style.display = visibleCount === 0 ? '' : 'none';
        }
    }

    if (searchInput) {
        searchInput.addEventListener('input', filterRows);
    }

    if (statusSelect) {
        statusSelect.addEventListener('change', filterRows);
    }
});

/**
 * Quick Status Change with SweetAlert2
 * @param {string} formId - Form ID to submit
 * @param {string} statusName - Target status
 * @param {number} orderId - Order ID
 */
window.confirmOrderStatusChange = function (formId, statusName, orderId) {
    if (typeof Swal !== 'undefined') {
        Swal.fire({
            title: `Update Order #${orderId}?`,
            text: `Change fulfillment status to "${statusName}"?`,
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#B67961',
            cancelButtonColor: '#64748B',
            confirmButtonText: 'Yes, update status',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                document.getElementById(formId)?.submit();
            }
        });
    } else {
        if (confirm(`Change Order #${orderId} status to "${statusName}"?`)) {
            document.getElementById(formId)?.submit();
        }
    }
};
