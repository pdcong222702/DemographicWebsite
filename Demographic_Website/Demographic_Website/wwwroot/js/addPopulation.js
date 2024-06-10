$(document).ready(function () {
    $('#addPopulation').click(function () {
        $.ajax({
            url: 'Admin/Populations/CreatePopulation',
            type: 'POST',
            success: function (res) {
                if (res.success) {
                    window.location.reload();
                }
            }
        });
    });
});