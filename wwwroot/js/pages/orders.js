/**
 * PAGE SCRIPT: ORDER HISTORY (Index.cshtml)
 * Live search and status filtering for customer orders
 */

document.addEventListener('DOMContentLoaded', () => {
    const searchInput = document.getElementById('orderSearchInput');
    const filterButtons = document.querySelectorAll('.order-filter-btn');
    const orderCards = document.querySelectorAll('.order-history-card');
    const noResults = document.getElementById('noOrdersFound');

    let currentStatus = 'all';

    function filterOrders() {
        const query = searchInput ? searchInput.value.toLowerCase().trim() : '';
        let visibleCount = 0;

        orderCards.forEach(card => {
            const cardStatus = card.getAttribute('data-status') || '';
            const cardText = card.textContent.toLowerCase();

            const matchesQuery = query === '' || cardText.includes(query);
            const matchesStatus = currentStatus === 'all' || cardStatus.toLowerCase() === currentStatus.toLowerCase();

            if (matchesQuery && matchesStatus) {
                card.style.display = '';
                visibleCount++;
            } else {
                card.style.display = 'none';
            }
        });

        if (noResults) {
            noResults.style.display = visibleCount === 0 ? '' : 'none';
        }
    }

    if (searchInput) {
        searchInput.addEventListener('input', filterOrders);
    }

    filterButtons.forEach(btn => {
        btn.addEventListener('click', () => {
            filterButtons.forEach(b => b.classList.remove('active'));
            btn.classList.add('active');
            currentStatus = btn.getAttribute('data-filter') || 'all';
            filterOrders();
        });
    });
});
