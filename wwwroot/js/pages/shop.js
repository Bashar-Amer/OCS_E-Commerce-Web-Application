/**
 * PAGE SCRIPT: SHOP (Index.cshtml)
 */

let activeCat = 'all';
let searchQuery = '';

function renderShopGrid() {
    const grid = document.getElementById('exactShopGrid');
    if (!grid) return;

    let products = [...window.BarrameruCatalog];

    if (activeCat && activeCat !== 'all') {
        products = products.filter(p => p.category.toLowerCase() === activeCat.toLowerCase());
    }

    if (searchQuery.trim() !== '') {
        const q = searchQuery.toLowerCase();
        products = products.filter(p => p.name.toLowerCase().includes(q) || p.category.toLowerCase().includes(q));
    }

    const sortElem = document.getElementById('shopSortingSelect');
    const sort = sortElem ? sortElem.value : 'default';
    if (sort === 'price-asc') products.sort((a,b) => a.price - b.price);
    if (sort === 'price-desc') products.sort((a,b) => b.price - a.price);
    if (sort === 'rating') products.sort((a,b) => b.rating - a.rating);

    grid.innerHTML = products.map(p => `
        <div class="col-md-3 col-6">
            <div class="template-product-card">
                ${p.badge ? `<span class="template-sale-tag">${p.badge}</span>` : ''}
                <img src="${p.image}" alt="${p.name}" class="template-product-img">
                <div>
                    <h6 class="template-product-title"><a href="/Shop/Details/${p.id}">${p.name}</a></h6>
                    ${p.rating ? `<div class="template-product-rating"><i class="bi bi-star-fill"></i><i class="bi bi-star-fill"></i><i class="bi bi-star-fill"></i><i class="bi bi-star-fill"></i><i class="bi bi-star-half"></i></div>` : '<div style="height: 19px;"></div>'}
                    <div class="template-product-price">
                        ${p.oldPrice ? `<span class="old-price">$${p.oldPrice.toFixed(2)}</span>` : ''}
                        $${p.price.toFixed(2)}
                    </div>
                </div>
                <button class="btn btn-template-add-cart" onclick="BarrameruStore.addToCart(${p.id}, 1)">Add to cart</button>
            </div>
        </div>
    `).join('');
}

document.addEventListener('DOMContentLoaded', () => {
    const searchInput = document.getElementById('shopSearchInput');
    if (searchInput && searchInput.value) {
        searchQuery = searchInput.value;
    }

    renderShopGrid();

    document.querySelectorAll('.shop-cat-filter').forEach(link => {
        link.addEventListener('click', function() {
            activeCat = this.getAttribute('data-cat');
            renderShopGrid();
        });
    });

    const btnSearch = document.getElementById('btnShopSearch');
    if (btnSearch) {
        btnSearch.addEventListener('click', () => {
            searchQuery = document.getElementById('shopSearchInput').value;
            renderShopGrid();
        });
    }

    const sortSelect = document.getElementById('shopSortingSelect');
    if (sortSelect) {
        sortSelect.addEventListener('change', renderShopGrid);
    }
});
