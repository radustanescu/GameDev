var selectedClass = "";

$(document).on('click', '.class-select', function () {
    $('.class-select').removeClass('class-selected');
    $('.class-select').addClass('class-select-default');

    $(this).removeClass('class-select-default');
    $(this).addClass('class-selected');
    selectedClass = $(this).data('class-id');
    console.log(selectedClass);
});

$(document).on('click', '.btn-choose', function () {
    if (selectedClass == "")
        alert('Esti un bou! Mai incearca!');

    Proxy.Class.CreateCharacter(selectedClass);
});