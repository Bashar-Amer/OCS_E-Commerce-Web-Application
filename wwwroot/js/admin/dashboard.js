/**
 * BARRAMERU ADMIN: DASHBOARD CHARTS & ANALYTICS (dashboard.js)
 * High-performance Chart.js integration for Revenue & Category Distribution
 */

document.addEventListener('DOMContentLoaded', () => {
    // Check if Chart.js is loaded
    if (typeof Chart === 'undefined') {
        console.warn('Chart.js is not loaded.');
        return;
    }

    // 1. REVENUE AREA CHART
    const revenueCanvas = document.getElementById('revenueChart');
    if (revenueCanvas) {
        const ctx = revenueCanvas.getContext('2d');

        // Create gradient fill
        const gradient = ctx.createLinearGradient(0, 0, 0, 240);
        gradient.addColorStop(0, 'rgba(182, 121, 97, 0.35)');
        gradient.addColorStop(1, 'rgba(182, 121, 97, 0.00)');

        let months = [];
        let revenueData = [];

        try {
            months = JSON.parse(revenueCanvas.getAttribute('data-labels') || '[]');
            revenueData = JSON.parse(revenueCanvas.getAttribute('data-revenue') || '[]');
        } catch (e) {
            console.error('Error parsing revenue chart data', e);
        }

        // Fallbacks if data is empty
        if (months.length === 0) {
            months = ['Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug'];
            revenueData = [0, 0, 0, 0, 185, 270];
        }

        new Chart(ctx, {
            type: 'line',
            data: {
                labels: months,
                datasets: [{
                    label: 'Store Revenue ($)',
                    data: revenueData,
                    borderColor: '#B67961',
                    borderWidth: 2.5,
                    pointBackgroundColor: '#B67961',
                    pointBorderColor: '#FFFFFF',
                    pointBorderWidth: 2,
                    pointRadius: 4,
                    pointHoverRadius: 6,
                    backgroundColor: gradient,
                    fill: true,
                    tension: 0.38
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: false
                    },
                    tooltip: {
                        backgroundColor: '#152A29',
                        titleColor: '#FFFFFF',
                        bodyColor: '#E2ECE9',
                        borderColor: '#B67961',
                        borderWidth: 1,
                        padding: 10,
                        displayColors: false,
                        callbacks: {
                            label: function (context) {
                                return 'Revenue: $' + context.parsed.y.toFixed(2);
                            }
                        }
                    }
                },
                scales: {
                    x: {
                        grid: {
                            display: false
                        },
                        ticks: {
                            color: '#64748B',
                            font: {
                                size: 11,
                                family: "'Plus Jakarta Sans', sans-serif"
                            }
                        }
                    },
                    y: {
                        grid: {
                            color: '#E2ECE9',
                            drawBorder: false
                        },
                        ticks: {
                            color: '#64748B',
                            font: {
                                size: 11,
                                family: "'Plus Jakarta Sans', sans-serif"
                            },
                            callback: function (value) {
                                return '$' + value;
                            }
                        }
                    }
                }
            }
        });
    }

    // 2. CATEGORY BREAKDOWN DOUGHNUT CHART
    const categoryCanvas = document.getElementById('categoryDoughnutChart');
    if (categoryCanvas) {
        let labels = [];
        let counts = [];

        try {
            labels = JSON.parse(categoryCanvas.getAttribute('data-labels') || '[]');
            counts = JSON.parse(categoryCanvas.getAttribute('data-counts') || '[]');
        } catch (e) {
            console.error('Error parsing category chart data', e);
        }

        // Fallbacks if data is empty
        if (labels.length === 0) {
            labels = ['Tents & Shelters', 'Backpacks & Bags', 'Sleeping Gear', 'Footwear', 'Accessories'];
            counts = [4, 5, 3, 2, 2];
        }

        const brandPalette = [
            '#B67961', // Terracotta
            '#152A29', // Dark Forest
            '#2D7A78', // Mint Teal
            '#F59E0B', // Amber Gold
            '#6366F1', // Indigo
            '#06B6D4', // Cyan
            '#EC4899', // Pink
            '#84CC16'  // Lime
        ];

        new Chart(categoryCanvas.getContext('2d'), {
            type: 'doughnut',
            data: {
                labels: labels,
                datasets: [{
                    data: counts,
                    backgroundColor: brandPalette.slice(0, labels.length),
                    borderColor: '#FFFFFF',
                    borderWidth: 2,
                    hoverOffset: 4
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '70%',
                plugins: {
                    legend: {
                        display: false
                    },
                    tooltip: {
                        backgroundColor: '#152A29',
                        titleColor: '#FFFFFF',
                        bodyColor: '#E2ECE9',
                        borderColor: '#B67961',
                        borderWidth: 1,
                        padding: 10,
                        callbacks: {
                            label: function (context) {
                                const total = context.dataset.data.reduce((a, b) => a + b, 0);
                                const percentage = total > 0 ? ((context.raw / total) * 100).toFixed(1) : 0;
                                return `${context.label}: ${context.raw} items (${percentage}%)`;
                            }
                        }
                    }
                }
            }
        });
    }
});
