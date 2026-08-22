/**
 * BARRAMERU ADMIN: CATEGORIES SCRIPT (categories.js)
 */

document.addEventListener('DOMContentLoaded', () => {
    // Client-side quick search filter for categories table
    const searchInput = document.getElementById('categorySearchInput');
    const tableBody = document.getElementById('categoriesTableBody');

    if (searchInput && tableBody) {
        searchInput.addEventListener('input', (e) => {
            const query = e.target.value.toLowerCase().trim();
            const rows = tableBody.querySelectorAll('tr.category-row');

            let visibleCount = 0;
            rows.forEach(row => {
                const nameText = row.querySelector('.category-name-cell')?.textContent.toLowerCase() || '';
                const idText = row.querySelector('.category-id-cell')?.textContent.toLowerCase() || '';

                if (nameText.includes(query) || idText.includes(query)) {
                    row.style.display = '';
                    visibleCount++;
                } else {
                    row.style.display = 'none';
                }
            });

            // Show empty state if no rows match
            const emptyRow = document.getElementById('noCategoryMatchRow');
            if (emptyRow) {
                emptyRow.style.display = visibleCount === 0 ? '' : 'none';
            }
        });
    }
});
