$(document).ready(function(){

  console.log("Javascript loaded");
  function ratingScript() {
    var str = document.getElementById("fire").innerHTML;
    var res = str.replace("&#x1F525", "&#x1F373");
    document.getElementById("fire").innerHTML = res;
  }
});
