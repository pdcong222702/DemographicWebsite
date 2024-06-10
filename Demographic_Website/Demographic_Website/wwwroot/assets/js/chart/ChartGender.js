$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Admin/StatisticalPopulation/ChartGender",
        data: "",
        contentType: "application/json;charset-utf-8",
        dataType: "json",
        success: OnSuccessGender,
    });

    function OnSuccessGender(data) {
        const chart = document.getElementById('totalGender');

        var _data = data;
        var _labels = _data[0];
        var _chartData = _data[1];

        var color = ['#4bc0c0', '#ff6384'];

        new Chart(chart, {
            type: 'pie',
            data: {
                labels: ['Nam', 'Nữ'],
                datasets: [{
                    backgroundColor: color,
                    data: data,
                    borderWidth: 1
                }]
            }
        });
    }
});