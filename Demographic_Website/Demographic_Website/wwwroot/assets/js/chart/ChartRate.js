$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Admin/StatisticalPopulation/ChartBirthRateDeathRate",
        data: "",
        contentType: "application/json;charset-utf-8",
        dataType: "json",
        success: OnSuccessChartBirthRateDeathRate,
    });

    function OnSuccessChartBirthRateDeathRate(data) {
        let _label = data[0];
        console.log(_label);
        const ChartBirthRateDeathRate = document.getElementById('ChartBirthRateDeathRate');
        var color = 'rgba(75, 192, 192, 0.6)';
        new Chart(ChartBirthRateDeathRate, {
            type: 'line',
            data: {
                labels: data[0],
                datasets: [
                    {
                        backgroundColor: color,
                        label: 'Tỷ lệ sinh',
                        data: data[1],
                        borderWidth: 4,
                        borderColor: color,
                    },
                    {
                        backgroundColor: '#ff6384',
                        label: 'Tỷ lệ tử',
                        data: data[2],
                        borderWidth: 4,
                        borderColor: '#ff6384',
                    }
                ]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: true,
                        text: 'Biểu đồ tỷ lệ sinh, tỷ lệ tử theo năm'
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            },
        });
    }
});