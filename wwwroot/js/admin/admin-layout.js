/**
 * BARRAMERU ADMIN CORE SCRIPT (admin-layout.js)
 */

document.addEventListener('DOMContentLoaded', () => {
    // 1. Mobile Sidebar Toggle & Backdrop
    const toggleBtn = document.querySelector('.admin-sidebar-toggle');
    const sidebar = document.querySelector('.admin-sidebar');
    const backdrop = document.querySelector('.admin-sidebar-backdrop');

    function closeSidebar() {
        if (sidebar) sidebar.classList.remove('show');
        if (backdrop) backdrop.classList.remove('show');
        document.body.classList.remove('sidebar-open');
    }

    function openSidebar() {
        if (sidebar) sidebar.classList.add('show');
        if (backdrop) backdrop.classList.add('show');
        document.body.classList.add('sidebar-open');
    }

    if (toggleBtn && sidebar) {
        toggleBtn.addEventListener('click', (e) => {
            e.stopPropagation();
            if (sidebar.classList.contains('show')) {
                closeSidebar();
            } else {
                openSidebar();
            }
        });

        if (backdrop) {
            backdrop.addEventListener('click', closeSidebar);
        }

        // Close on escape key
        document.addEventListener('keydown', (e) => {
            if (e.key === 'Escape' && sidebar.classList.contains('show')) {
                closeSidebar();
            }
        });

        // Close when clicking nav link on mobile
        document.querySelectorAll('.admin-nav-link').forEach(link => {
            link.addEventListener('click', () => {
                if (window.innerWidth < 992) {
                    closeSidebar();
                }
            });
        });
    }

    // 2. Active Sidebar Link Highlighting
    const currentPath = window.location.pathname.toLowerCase();
    const navLinks = document.querySelectorAll('.admin-nav-link');

    navLinks.forEach(link => {
        const linkPath = link.getAttribute('href')?.toLowerCase();
        if (linkPath && linkPath !== '#' && linkPath !== '') {
            if (currentPath === linkPath || (linkPath !== '/admin' && currentPath.startsWith(linkPath))) {
                link.classList.add('active');
            }
        }
    });
});

/**
 * Reusable SweetAlert2 Toast Notification
 * @param {string} title - The notification message
 * @param {'success'|'error'|'warning'|'info'} icon - Notification type
 */
window.showAdminToast = function (title, icon = 'success') {
    if (typeof Swal !== 'undefined') {
        const Toast = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3500,
            timerProgressBar: true,
            didOpen: (toast) => {
                toast.onmouseenter = Swal.stopTimer;
                toast.onmouseleave = Swal.resumeTimer;
            }
        });
        Toast.fire({
            icon: icon,
            title: title
        });
    }
};

/**
 * Reusable SweetAlert2 Delete Confirmation
 * @param {string} formId - ID of the form to submit upon confirmation
 * @param {string} itemName - Name of the item being deleted
 */
function confirmAdminDelete(formId, itemName = 'this item') {
    if (typeof Swal !== 'undefined') {
        Swal.fire({
            title: 'Are you sure?',
            text: `Do you really want to remove "${itemName}"? This action can be undone by an administrator.`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#EF4444',
            cancelButtonColor: '#64748B',
            confirmButtonText: 'Yes, remove it!',
            cancelButtonText: 'Cancel',
            reverseButtons: true,
            customClass: {
                popup: 'rounded-0 shadow-lg',
                confirmButton: 'rounded-0',
                cancelButton: 'rounded-0'
            }
        }).then((result) => {
            if (result.isConfirmed) {
                document.getElementById(formId)?.submit();
            }
        });
    } else {
        if (confirm(`Are you sure you want to remove "${itemName}"?`)) {
            document.getElementById(formId)?.submit();
        }
    }
}
