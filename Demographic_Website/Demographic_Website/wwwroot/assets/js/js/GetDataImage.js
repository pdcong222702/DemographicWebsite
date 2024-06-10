$(document).ready(function () {

    function ClickUpload() {
        $("#Image").click();
    }

    function SelectFileUpload() {
        var file = document.getElementById('file-upload');
        var image = document.getElementById('Image');
        image = file.files[0];
        console.log(image);
    }
});