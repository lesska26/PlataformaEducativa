class Cedulas {
    Verificar() {
        let cedula = document.getElementById("Cedula");
        if (cedula.value.trim().length <= 0) {
            return false;
            
        }
       
        
    }

    Crear() {
        let Nombre = document.getElementById("Nombre").value;
        let Apellido = document.getElementById("Apellido").value;
        let Telefono = document.getElementById("Telefono").value;
        let Correo = document.getElementById("Correo").value;
        let Fecha = document.getElementById("Fecha").value;
        let Cedulas = document.getElementById("Cedulas").value;
        let IdCurso = document.getElementById("IdCurso").value;
        let IdInstituto = document.getElementById("IdInstituto").value;
        let iniciarCursoId = document.getElementById("iniciarCursoId").value;
        let Hombre = document.querySelector("#H");
        let Mujer = document.querySelector("#M");
        let Genero = '';
        let paso = true;
        if (Hombre.checked == true || Mujer.checked == true) {
            Genero = Hombre.checked == true ? Hombre.value : Mujer.value;
            document.getElementById("spanGenero").textContent = "";


        } else {
            document.getElementById("spanGenero").textContent="Tienes que elegir un Genero";
            paso = false;
        }
        if (Nombre.trim().length <= 0) {
            document.getElementById("spanNombre").textContent = "No puedes dejar el campo vacio";
            paso = false;
        } else {
            document.getElementById("spanNombre").textContent = "";
        }
        if (Apellido.trim().length <=0) {
            document.getElementById("spanApellido").textContent = "No puedes dejar el campo vacio";
            paso = false;
        } else {
            document.getElementById("spanApellido").textContent = "";
        }
        if (Correo.trim().length <=0) {
            document.getElementById("spanEmail").textContent = "No puedes dejar el campo vacio";
            paso = false;
        } else {
            document.getElementById("spanEmail").textContent = "";
        }
        if (Telefono.trim().length <=0) {
            document.getElementById("spanTelefono").textContent = "No puedes dejar el campo vacio";
        } else {
            document.getElementById("spanTelefono").textContent = "";
        }

        if (paso) {
           var Datos =
           {
               Nombre,
               Apellido,
               Telefono,
               Correo,
               Fecha,
               Cedulas,
               IdCurso,
               IdInstituto,
               iniciarCursoId,
               Genero
           };
                console.log(Datos);
                let json = JSON.stringify(Datos);
                let xhr = new XMLHttpRequest();
                xhr.open("POST", "/Estudiantes/Crear");
                xhr.addEventListener("load", f => {
                    if (xhr.status == 200) {
                        if (xhr.response == "true") {
                            window.location.href = "/IniciarCurso/Index";
                        }
                        if (xhr.response == "existe") {

                            document.getElementById("spanCedula").style.display = "block";
                        }

                    }

                })
                xhr.setRequestHeader("Content-type", "application/json");
                xhr.send(json);

            }
       
    }
}

document.getElementById("formExiste").addEventListener("submit",
    e => {

        e.preventDefault();

        var cedula = new Cedulas();
        cedula.Verificar();
        if (cedula.Verificar() == false) {
            alert("no puedes dejar el campo vacio");
        } else {
            
            var xhr = new XMLHttpRequest();

            xhr.addEventListener("load", f => {
                //if (xhr.responseText == "false") {
                //    console.log("no  existe esa cedula");
                //}
                if (xhr.responseText != "true") {
                    document.getElementById("formulario").innerHTML = xhr.responseText;




                    document.getElementById("btnCrear").addEventListener("click", new Cedulas().Crear)

                } if (xhr.responseText =="true")
                {
                    window.location.href = "/IniciarCurso/Index";
                }
            })
            xhr.open("POST","/Estudiantes/Existe")
            xhr.setRequestHeader("Content-Type", "application/json");
            let cedula = document.getElementById("Cedula").value;
            let iniciarCursoId = document.getElementById("iniciarCursoId").value;
            let Datos = {
                Cedula: cedula,
                iniciarCursoId
            };
            let Cursos = JSON.stringify(Datos);
            xhr.send(Cursos);
        }
    })



