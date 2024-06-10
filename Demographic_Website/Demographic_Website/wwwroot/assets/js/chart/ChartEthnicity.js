$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Admin/StatisticalPopulation/ChartEthnicity",
        data: "",
        contentType: "application/json;charset-utf-8",
        dataType: "json",
        success: OnSuccessChartEthnicity,
    });
    function OnSuccessChartEthnicity(data) {
        const chartEthnicity = document.getElementById('ChartEthnicity');
        //console.log(data);
        var EthnicityVietNam = [
            "Kinh",
            "Tày",
            "Thái",
            "Mường",
            "Hoa",
            "Hmông",
            "Khơ Mú",
            "Dân tộc khác"
        ];
        var color = 'rgba(75, 192, 192, 0.6)';
        new Chart(chartEthnicity, {
            type: 'bar',
            data: {
                labels: EthnicityVietNam,
                datasets: [
                    {
                        backgroundColor: color,
                        label: 'Nam',
                        data: data[0],
                        borderWidth: 1,
                        borderColor: color,
                    },
                    {
                        backgroundColor: '#ff6384',
                        label: 'Nữ',
                        data: data[1],
                        borderWidth: 1,
                        borderColor: '#ff6384',
                    },
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