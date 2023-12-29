class Validar
{
    validarDia()
    {
        var dia = [];
        var diaCaja = document.getElementById("cajaDia").querySelectorAll("input");
        console.log(diaCaja);
        diaCaja.forEach(e => {
            if (e.checked)
            {
                dia.push(e.value);
            }
        })

        if (dia.length > 0) {
            document.getElementById("DiaValidar").textContent ="";
            return dia;
        }
        document.getElementById("DiaValidar").textContent = "Elija un Dia";
        return false;
    }
    validarForm()
    {
        let validar = true;
        let Instituto = document.getElementById("Instituto");
        let curso = document.getElementById("NombreCurso");
        let Horas = document.getElementById("Horas");
        if (Instituto.value == "0") {
            validar = false;
            document.getElementById("spanInstituto").textContent = "Elija un Instituto";
        } else {
            document.getElementById("spanInstituto").textContent ="";
        }
        if (curso.value == "0") {
            document.getElementById("spanNombreCurso").textContent = "Elija un Curso";
            validar = false;
        } else {
            document.getElementById("spanNombreCurso").textContent = "";
        }

        if (Horas.value == "0") {
            validar = false;
            document.getElementById("spanHoras").textContent = "Elija una Hora";
        } else {
            document.getElementById("spanHoras").textContent = "";
        }
        return validar;
    }

}


document.getElementById("formulario").addEventListener("submit", e => {
    e.preventDefault();
    console.log(new Validar().validarDia())
    console.log(new Validar().validarForm())

    if (new Validar().validarDia != false && new Validar().validarForm() == true)
    {
        var xht = new XMLHttpRequest();
        xht.open('POST', "/IniciarCurso/Crear");
        xht.setRequestHeader("Content-Type", "Application/json");
        xht.onreadystatechange = (event) => {
            if (xht.status == 200)
            {
                window.location.href = "/IniciarCurso/Index";
                console.log('todo salio bien');
            }
            
        }
        var objc =
        {
            NombreCurso: document.getElementById("NombreCurso").value,
            Instituto: document.getElementById("Instituto").value,
            Horas: document.getElementById("Horas").value,
            diaSemanass: new Validar().validarDia()
        }

        var json = JSON.stringify(objc);
        console.log(objc);
        xht.send(json);
    }

})


