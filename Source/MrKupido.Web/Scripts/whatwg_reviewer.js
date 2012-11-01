/* http://www.whatwg.org/specs/web-apps/current-work/reviewer.js */

var reviewer;
var reviewSectionId;

function submitReviewComment(textField, button) {
    var text = document.getElementById('reviewCommentText').value;
    if ((!text) || (text.length <= 5) || (text.indexOf(' ') == text.lastIndexOf(' '))) return; 
    
    var x = new XMLHttpRequest();
    x.open('POST', '~/BugReport');
    x.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    x.onreadystatechange = function () {
        if (x.readyState == 4) {
            if (x.status == 200 && x.responseText != 'ERROR') {
                showAlert('Thank you for helping the HTML5 effort! Your comment was filed as bug ' + x.responseText + '. You can see it at:', 'https://www.w3.org/Bugs/Public/show_bug.cgi?id=' + x.responseText);
            } else {
                showAlert('An error occured while submitting your comment. Please let ian@hixie.ch know.');
            }
            button.disabled = false;
            textField.select();
        }
    };
    x.send('id=' + encodeURIComponent(reviewSectionId) + '&text=' + encodeURIComponent(text) + opt_component + s_login);
    button.disabled = true;
}
