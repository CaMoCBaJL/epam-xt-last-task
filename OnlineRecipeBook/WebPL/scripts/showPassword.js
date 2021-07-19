
function showPassword(elemId) {
    var c = elemId;

    if (document.getElementById(elemId).type == 'password') {
        document.getElementById(elemId).type = 'text'
    }
    else {
        document.getElementById(elemId).type = 'password'
    }
}
