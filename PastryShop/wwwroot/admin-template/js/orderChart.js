document.addEventListener("DOMContentLoaded", function () {
    var orderLabels = JSON.parse(document.getElementById('orderChartLabels').value);
    var orderData = JSON.parse(document.getElementById('orderChartData').value);

    var ctx = document.getElementById('orderChart').getContext('2d');
    var orderChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: orderLabels,
            datasets: [{
                label: 'Sipariş Sayısı',
                data: orderData,
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 2,
                fill: true,
                tension: 0.3
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    precision: 0
                }
            }
        }
    });
});