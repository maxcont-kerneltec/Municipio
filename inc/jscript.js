function formatar(src, mask) {
  var i = src.value.length;
  var saida = mask.substring(0, 1);
  var texto = mask.substring(i)

  if (texto.substring(0, 1) != saida) {
    src.value += texto.substring(0, 1);
  }
}

//aceita somente números em uma text
function SomenteNumero(e) {
  var tecla = (window.event) ? event.keyCode : e.which;
  if ((tecla > 47 && tecla < 58)) return true;
  else {
    if (tecla == 8 || tecla == 0) return true;
    else return false;
  }
}

function Formata_Data(campo, e) {
  var whichCode = (window.event) ? e.keyCode : e.which;
  var tamanho = document.getElementById(campo).value.length
  if ((tamanho == 2 || tamanho == 5) && whichCode == 47) { return (false); }

  if ((tamanho == 4 && whichCode == 111) || (tamanho == 7 && whichCode == 111)) {
    document.getElementById(campo).value = document.getElementById(campo).value.substring(0, tamanho - 1)
    return (false)
  }
  if ((whichCode < 48 || whichCode > 57) && (whichCode < 96 || whichCode > 105) && (whichCode != 8 && whichCode != 9 && whichCode != 13 && whichCode != 46)) {
    return (false)
  }

  if (tamanho == 2 && whichCode == 111) {
    document.getElementById(campo).value = "0" + document.getElementById(campo).value
    return (false)
  }

  if (tamanho == 5 && whichCode == 111) {
    var_inicio = document.getElementById(campo).value.substring(0, 3)
    document.getElementById(campo).value = document.getElementById(campo).value.substring(0, 3) + "0" + document.getElementById(campo).value.substring(3, 4)
  }

  if ((whichCode != 8) && (tamanho == 2 || tamanho == 5)) {
    document.getElementById(campo).value = document.getElementById(campo).value + "/";
  }
}

function SoNumero(campo, e) {
  var whichCode = (window.event) ? e.keyCode : e.which;
  var tamanho = document.getElementById(campo).value.length

  if ((whichCode < 48 || whichCode > 57) && (whichCode < 96 || whichCode > 105) && (whichCode != 8 && whichCode != 9 && whichCode != 13 && whichCode != 46)) {
    //document.getElementById(campo).value = document.getElementById(campo).value.substring(0, tamanho - 1)
    return (false)
  }
}