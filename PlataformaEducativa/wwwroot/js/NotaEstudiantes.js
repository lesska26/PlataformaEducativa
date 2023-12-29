document.querySelector("#Calificar").addEventListener("click", e => {
    let validar = true;
    let ListaNota = [];
    document.querySelectorAll("input").forEach(e => {
        let numero = Number(e.value);
        if (!isNaN(numero)) {
            ListaNota.push({
                Nota: numero,
                EstudianteId: e.id,
                IniciarCursoId: e.name
            })
        } else {
            validar = false;
        }
        
    })
    if (validar == false) {
        alert("no puede dejar campos vacio");
    } else {
        let xhr = new  XMLHttpRequest();
        xhr.open("POST", "/Calificar/CalificarEstudiantes");
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.addEventListener("load", () => {
            if (xhr.status == 200) {

                if (xhr.response == "true") {
                    window.location.href = "/Home/Index";
                }
                else {
                    alert(xhr.response);
                }
            } else {
                console.log(xhr.response)
            }
        })
        let json = JSON.stringify(ListaNota);
        xhr.send(json);
    }

    console.log(ListaNota);
})

document.querySelectorAll("input").forEach(f => {
    f.addEventListener("input", ValidarNumero);
})

function ValidarNumero(event) {
    var inputValue = event.target.value;

   
    var soloNumeros = inputValue.replace(/[^0-9]/g, '');
    soloNumeros = soloNumeros.substring(0, 3);
    soloNumeros = Math.min(parseInt(soloNumeros), 100);
    event.target.value = soloNumeros;
   
    
}