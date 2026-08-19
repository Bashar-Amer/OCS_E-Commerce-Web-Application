/**
 * PAGE SCRIPT: PRODUCT DETAILS (Details.cshtml)
 */

document.addEventListener('DOMContentLoaded', () => {
    const urlParams = window.location.pathname.split('/');
    const prodIdFromUrl = parseInt(urlParams[urlParams.length - 1]);
    const currentId = !isNaN(prodIdFromUrl) && prodIdFromUrl > 0 ? prodIdFromUrl : 2;

    const prod = window.BarrameruCatalog ? (window.BarrameruCatalog.find(p => p.id === currentId) || window.BarrameruCatalog[1]) : null;
    
    if (prod) {
        const titleElem = document.getElementById('detailsPageTitle');
        if (titleElem) titleElem.textContent = prod.name;
        
        const subTitleElem = document.getElementById('detailsPageSubTitle');
        if (subTitleElem) subTitleElem.textContent = prod.category;
        
        const prodTitle = document.getElementById('prodTitle');
        if (prodTitle) prodTitle.textContent = prod.name;
        
        const prodDesc = document.getElementById('prodDesc');
        if (prodDesc) prodDesc.textContent = prod.description;
        
        const prodPrice = document.getElementById('prodPrice');
        if (prodPrice) prodPrice.textContent = `$${prod.price.toFixed(2)}`;
        
        const prodOldPrice = document.getElementById('prodOldPrice');
        if (prodOldPrice) {
            if (prod.oldPrice) {
                prodOldPrice.textContent = `$${prod.oldPrice.toFixed(2)}`;
                prodOldPrice.style.display = 'inline';
            } else {
                prodOldPrice.style.display = 'none';
            }
        }
        
        const prodCategory = document.getElementById('prodCategory');
        if (prodCategory) prodCategory.textContent = prod.category;
        
        const mainImg = document.getElementById('mainDetailImg');
        if (mainImg) mainImg.src = prod.image;

        // Add to cart button
        const btnAdd = document.getElementById('btnAddToCartMain');
        if (btnAdd) {
            btnAdd.addEventListener('click', () => {
                const qtyInput = document.getElementById('detailQtyInput');
                const qty = qtyInput ? (parseInt(qtyInput.value) || 1) : 1;
                BarrameruStore.addToCart(prod.id, qty);
            });
        }

        // Wishlist button
        const btnWish = document.getElementById('btnWishlistMain');
        if (btnWish) {
            btnWish.addEventListener('click', () => {
                BarrameruStore.toggleWishlist(prod.id);
            });
        }
    }

    // Thumbnail switching
    document.querySelectorAll('#detailThumbnailsRow img').forEach(img => {
        img.addEventListener('click', function() {
            const mainImg = document.getElementById('mainDetailImg');
            if (mainImg) mainImg.src = this.src;
            document.querySelectorAll('#detailThumbnailsRow img').forEach(i => i.classList.remove('detail-thumb-active'));
            this.classList.add('detail-thumb-active');
        });
    });
});
