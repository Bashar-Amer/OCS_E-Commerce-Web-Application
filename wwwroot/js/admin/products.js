/**
 * BARRAMERU ADMIN: PRODUCTS SCRIPT (products.js)
 * Option 1: Two explicit parameters (selectedExistingImageId & selectedNewImageIndex)
 */

// Global function for existing images in Edit view
window.selectExistingMainImage = function (cardElement, radioId) {
    const radio = document.getElementById(radioId);
    if (radio) {
        radio.checked = true;
    }

    // 1. Highlight this existing card
    document.querySelectorAll('.existing-image-card').forEach(c => {
        c.classList.remove('is-main');
        const badge = c.querySelector('.existing-main-badge');
        if (badge) badge.textContent = 'Set Cover';
    });
    cardElement.classList.add('is-main');
    const badge = cardElement.querySelector('.existing-main-badge');
    if (badge) badge.textContent = '★ Main Cover';

    // 2. Unset Cover status from newly uploaded preview thumbnails
    if (window.unsetNewUploadsCover) {
        window.unsetNewUploadsCover();
    }
};

// Global function to navigate to RemoveImage action for permanent deletion
window.confirmRemoveImage = function (ev, imageId) {
    ev.stopPropagation();
    if (typeof Swal !== 'undefined') {
        Swal.fire({
            title: 'Delete Photo?',
            text: 'Are you sure you want to permanently remove this product photo?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#EF4444',
            cancelButtonColor: '#64748B',
            confirmButtonText: 'Yes, delete it'
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = `/Admin/Products/RemoveImage/${imageId}`;
            }
        });
    } else {
        if (confirm('Delete this photo permanently?')) {
            window.location.href = `/Admin/Products/RemoveImage/${imageId}`;
        }
    }
};

document.addEventListener('DOMContentLoaded', () => {
    // 1. Table Live Filtering
    const searchInput = document.getElementById('productSearchInput');
    const tableBody = document.getElementById('productsTableBody');
    const categorySelect = document.getElementById('productCategoryFilter');

    function filterRows() {
        const query = searchInput?.value.toLowerCase().trim() || '';
        const selectedCat = categorySelect?.value || '';
        const rows = tableBody?.querySelectorAll('tr.product-row') || [];

        let visibleCount = 0;
        rows.forEach(row => {
            const nameText = row.querySelector('.product-name-cell')?.textContent.toLowerCase() || '';
            const catId = row.getAttribute('data-category-id') || '';
            const idText = row.querySelector('.product-id-cell')?.textContent.toLowerCase() || '';

            const matchesQuery = nameText.includes(query) || idText.includes(query);
            const matchesCat = selectedCat === '' || catId === selectedCat;

            if (matchesQuery && matchesCat) {
                row.style.display = '';
                visibleCount++;
            } else {
                row.style.display = 'none';
            }
        });

        const emptyRow = document.getElementById('noProductMatchRow');
        if (emptyRow) {
            emptyRow.style.display = visibleCount === 0 ? '' : 'none';
        }
    }

    if (searchInput) {
        searchInput.addEventListener('input', filterRows);
    }

    if (categorySelect) {
        categorySelect.addEventListener('change', () => {
            const selectedVal = categorySelect.value;
            if (selectedVal) {
                window.location.href = `/Admin/Products?categoryId=${selectedVal}`;
            } else {
                window.location.href = `/Admin/Products`;
            }
        });
    }

    // 2. Multi-Image Upload Manager with Explicit Separate Inputs
    const fileInput = document.getElementById('productImageFilesInput');
    const previewContainer = document.getElementById('imagePreviewContainer');
    const dropzone = document.getElementById('imageDropzone');
    const newIndexInput = document.getElementById('selectedNewImageIndexInput');

    let accumulatedFiles = [];
    
    // Check if there are existing photos on the page
    const hasExistingPhotos = document.querySelectorAll('.existing-image-card').length > 0;
    let mainImageIndex = hasExistingPhotos ? -1 : 0;

    window.unsetNewUploadsCover = function () {
        mainImageIndex = -1;
        syncNewIndexInput();
        renderPreviews();
    };

    function syncNewIndexInput() {
        if (newIndexInput) {
            newIndexInput.value = (mainImageIndex >= 0) ? mainImageIndex : '';
        }
    }

    function setMainImage(index) {
        mainImageIndex = index;
        syncNewIndexInput();

        // When a new upload is chosen as cover, uncheck all existing photos
        document.querySelectorAll('.existing-image-card').forEach(c => {
            c.classList.remove('is-main');
            const r = c.querySelector('input[type="radio"]');
            if (r) r.checked = false;
            const b = c.querySelector('.existing-main-badge');
            if (b) b.textContent = 'Set Cover';
        });

        renderPreviews();
    }

    function renderPreviews() {
        if (!previewContainer) return;

        previewContainer.innerHTML = '';

        if (accumulatedFiles.length > 0) {
            previewContainer.style.setProperty('display', 'flex', 'important');

            accumulatedFiles.forEach((file, index) => {
                const reader = new FileReader();
                reader.onload = function (e) {
                    const isMain = (index === mainImageIndex);
                    const thumb = document.createElement('div');
                    thumb.className = `image-preview-thumb ${isMain ? 'is-main' : ''}`;
                    thumb.title = isMain ? 'Primary Cover Photo' : 'Click to set as Cover Photo';
                    thumb.innerHTML = `
                        <img src="${e.target.result}" alt="Photo ${index + 1}" />
                        <span class="thumb-main-badge">${isMain ? '★ Cover' : 'Set Cover'}</span>
                        <button type="button" class="btn-remove-preview" title="Remove this photo">
                            <i class="bi bi-x"></i>
                        </button>
                    `;

                    // Click thumbnail to make it Main Cover
                    thumb.addEventListener('click', (ev) => {
                        if (!ev.target.closest('.btn-remove-preview')) {
                            setMainImage(index);
                        }
                    });

                    // Remove button
                    thumb.querySelector('.btn-remove-preview')?.addEventListener('click', (ev) => {
                        ev.stopPropagation();
                        removeImageAtIndex(index);
                    });

                    previewContainer.appendChild(thumb);
                };
                reader.readAsDataURL(file);
            });
        } else {
            previewContainer.style.setProperty('display', 'none', 'important');
        }
    }

    function updateFileInput() {
        if (!fileInput) return;
        const dataTransfer = new DataTransfer();
        accumulatedFiles.forEach(file => dataTransfer.items.add(file));
        fileInput.files = dataTransfer.files;
    }

    function addFiles(newFiles) {
        Array.from(newFiles).forEach(file => {
            if (file.type.startsWith('image/')) {
                const exists = accumulatedFiles.some(f => f.name === file.name && f.size === file.size);
                if (!exists) {
                    accumulatedFiles.push(file);
                }
            }
        });
        updateFileInput();
        
        // If there are no existing photos and no main chosen, make first new photo main
        const existingCount = document.querySelectorAll('.existing-image-card').length;
        if (existingCount === 0 && mainImageIndex === -1 && accumulatedFiles.length > 0) {
            mainImageIndex = 0;
        }

        syncNewIndexInput();
        renderPreviews();
    }

    function removeImageAtIndex(index) {
        accumulatedFiles.splice(index, 1);
        if (mainImageIndex === index) {
            mainImageIndex = accumulatedFiles.length > 0 ? 0 : -1;
        } else if (mainImageIndex > index) {
            mainImageIndex--;
        }
        updateFileInput();
        syncNewIndexInput();
        renderPreviews();
    }

    if (fileInput) {
        fileInput.addEventListener('change', function () {
            if (this.files && this.files.length > 0) {
                addFiles(this.files);
            }
        });

        const form = fileInput.closest('form');
        if (form) {
            form.addEventListener('submit', () => {
                updateFileInput();
                syncNewIndexInput();
            });
        }
    }

    // Drag & Drop
    if (dropzone) {
        ['dragenter', 'dragover'].forEach(eventName => {
            dropzone.addEventListener(eventName, (e) => {
                e.preventDefault();
                dropzone.classList.add('border-primary');
            }, false);
        });

        ['dragleave', 'drop'].forEach(eventName => {
            dropzone.addEventListener(eventName, (e) => {
                e.preventDefault();
                dropzone.classList.remove('border-primary');
            }, false);
        });

        dropzone.addEventListener('drop', (e) => {
            const dt = e.dataTransfer;
            if (dt.files && dt.files.length > 0) {
                addFiles(dt.files);
            }
        });
    }
});
