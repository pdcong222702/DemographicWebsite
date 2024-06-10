$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Admin/StatisticalPopulation/ChartAge",
        data: "",
        contentType: "application/json;charset-utf-8",
        dataType: "json",
        success: OnSuccessChartAge,
    });
    function OnSuccessChartAge(data) {
        const chartAge = document.getElementById('ChartAge');

        console.log(data);

        var color = ['rgba(75, 192, 192, 0.6)', 'rgba(255, 159, 64, 0.6)', 'rgba(255, 99, 132, 0.6)'];

        new Chart(chartAge, {
            type: 'bar',
            data: {
                labels: ['Dưới 18 tuổi', 'Từ 18-60 tuổi'],
                datasets: [
                    {
                        label: 'Nam',
                        data: data[0],
                        borderColor: color[0],
                        backgroundColor: color[0],
                    },
                    {
                        label: 'Nữ',
                        data: data[1],
                        borderColor: color[1],
                        backgroundColor: color[1],
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
                        text: 'Thống kê dân tộc'
                    }
                }
            },
        });

        
    }
});