/**
 * BARRAMERU ADMIN CORE SCRIPT (admin-layout.js)
 */

document.addEventListener('DOMContentLoaded', () => {
    // 1. Mobile Sidebar Toggle
    const toggleBtn = document.querySelector('.admin-sidebar-toggle');
    const sidebar = document.querySelector('.admin-sidebar');

    if (toggleBtn && sidebar) {
        toggleBtn.addEventListener('click', () => {
            sidebar.classList.toggle('show');
        });

        // Close sidebar when clicking outside on mobile
        document.addEventListener('click', (e) => {
            if (window.innerWidth < 992) {
                if (!sidebar.contains(e.target) && !toggleBtn.contains(e.target) && sidebar.classList.contains('show')) {
                    sidebar.classList.remove('show');
                }
            }
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
