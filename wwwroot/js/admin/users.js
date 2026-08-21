/**
 * BARRAMERU ADMIN: USERS SCRIPT (users.js)
 * Live real-time table searching for registered customers & users
 */

document.addEventListener('DOMContentLoaded', () => {
    const searchInput = document.getElementById('userSearchInput');
    const tableBody = document.getElementById('usersTableBody');

    function filterRows() {
        const query = searchInput?.value.toLowerCase().trim() || '';
        const rows = tableBody?.querySelectorAll('tr.user-row') || [];

        let visibleCount = 0;
        rows.forEach(row => {
            const nameText = row.querySelector('.user-name-cell')?.textContent.toLowerCase() || '';
            const emailText = row.querySelector('.user-email-cell')?.textContent.toLowerCase() || '';
            const phoneText = row.querySelector('.user-phone-cell')?.textContent.toLowerCase() || '';
            const idText = row.querySelector('.user-id-cell')?.textContent.toLowerCase() || '';

            const matchesQuery = nameText.includes(query) || 
                                 emailText.includes(query) || 
                                 phoneText.includes(query) || 
                                 idText.includes(query);

            if (matchesQuery) {
                row.style.display = '';
                visibleCount++;
            } else {
                row.style.display = 'none';
            }
        });

        const emptyRow = document.getElementById('noUserMatchRow');
        if (emptyRow) {
            emptyRow.style.display = visibleCount === 0 ? '' : 'none';
        }
    }

    if (searchInput) {
        searchInput.addEventListener('input', filterRows);
    }
});
