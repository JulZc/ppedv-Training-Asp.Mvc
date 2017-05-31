setInterval(function(){

var xhttp = new XMLHttpRequest();

xhttp.open("GET", "http://localhost:54246/api/movie", true);
xhttp.send();

xhttp.onreadystatechange = function() {
    if (this.readyState == 4 && this.status == 200) {
       // Typical action to be performed when the document is ready:
       console.log( xhttp.responseText);
    }
};

}, 500);