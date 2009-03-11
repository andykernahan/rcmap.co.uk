var _currentHelp = "SubmitHelp";

function ShowHelp(e) {
    $(_currentHelp).hide();
    _currentHelp = e + "Help";
    $(_currentHelp).show();
}