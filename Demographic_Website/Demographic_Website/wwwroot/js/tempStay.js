//Random
var generatedNumbers = []; // Danh sách để lưu các số đã tạo

function generateUniqueRandomNumber() {
    var randomNumber;
    do {
        randomNumber = Math.floor(Math.random() * 1000000); // Tạo số ngẫu nhiên từ 0 đến 99
    } while (generatedNumbers.includes(randomNumber));

    generatedNumbers.push(randomNumber); // Thêm số mới vào danh sách

    return randomNumber;
}

function setRandomNumber() {
    var randomNumber = generateUniqueRandomNumber();
    document.getElementById("randomInput").value = randomNumber;
}