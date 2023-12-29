class Usuario {

    ExisteGestor() {
        
    }
}

document.querySelector("form").addEventListener("submit", e => {
    let Roles = document.querySelector("#Roles");
    let Instituciones = document.querySelector("#selectInstituciones");
    let validar = true;
    let genero = false;
    let cedula = document.getElementById("cedula");
    let Nombre = document.getElementById("Nombre");
    let Apellido = document.getElementById("Apellido");
    let Correo = document.getElementById("Correo");
    let Telefono = document.getElementById("Telefono");
    let FechaNacimiento = document.getElementById("FechaNacimiento");
    let spanNombre = document.getElementById("spanNombre");
    let spanApellido = document.getElementById("spanApellido");
    let spanCorreo = document.getElementById("spanCorreo");
    let spanTelefono = document.getElementById("spanTelefono");
    let spanCedula = document.getElementById("spanCedula");
    let expNombre = /^[a-zA-ZÀ-ÿ]+(?:\s[a-zA-ZÀ-ÿ]+)?(?:\s[a-zA-ZÀ-ÿ]+)?$/;
    let expApellido = /^[a-zA-ZÀ-ÿ]+(?:\s[a-zA-ZÀ-ÿ]+)?(?:\s[a-zA-ZÀ-ÿ]+)?$/;
    let exCorreo = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    let expCedula = /^\d{3}\d{7}\d{1}$/;
    let expTelefono = /^\d{6,15}$/;
    if (!expCedula.test(cedula.value)) {
        validar = false;
        spanCedula.textContent = "Coloque una Cedula Valida";
    } else {
        spanCedula.textContent = "";
    }
    if (!expNombre.test(Nombre.value)) {
        validar = false;
        spanNombre.textContent = "Coloque un Nombre Valido";
    } else {
        spanNombre.textContent = "";
    }

    if (!expApellido.test(Apellido.value)) {
        validar = false;
        spanApellido.textContent = "Coloque un Apellido Valido";
    } else {
        spanApellido.textContent = "";
    }
    if (!exCorreo.test(Correo.value)) {
        validar = false;
        spanCorreo.textContent = "Coloque un Correo Valido";
    } else {
        spanCorreo.textContent = "";
    }
    if (!expTelefono.test(Telefono.value)) {
        validar = false;
        spanTelefono.textContent = "Coloque un Telefono Valido";
    }
    if (!ValidarFecha(FechaNacimiento)) {
        validar = false;
    }
    if (validar) {
        if (Instituciones.value.trim().length > 0 && Roles.value.trim().length > 0) {
            console.log(Roles.value)
            console.log(Instituciones.value);
            let xhr = new XMLHttpRequest();
            xhr.open("GET", `/Usuario/ExisteGestor?Instituciones=${Instituciones.value}&Roles=${Roles.value}`, false)
            xhr.addEventListener("load", () => {
                if (xhr.status == 200) {
                    if (xhr.response == "true") {
                        document.querySelector("#spanExiste").textContent = "Existe ese Gestor en ese Instituto";
                        e.preventDefault();
                        console.log(xhr.response)
                    } else {

                        document.querySelector("#spanExiste").textContent = "Esta Disponible";
                        console.log(xhr.response)
                    }
                }
            })

            xhr.send();
        } else {
            e.preventDefault();
            alert("Tienes que Elegir un Centro");
        }


    } else {
        e.preventDefault();
    }
    
    
   
})






document.getElementById("Municipio").addEventListener("click", e => {
    //console.log(e.target.value)
    if (e.target.value.trim().length > 0) {
        let select = document.getElementById("selectInstituciones");
        let xhr = new XMLHttpRequest();
        xhr.open("GET", `/Usuario/listInstituto?Municipios=${e.target.value}`)
        //xhr.setRequestHeader("Content-Type","application-json")
        xhr.addEventListener("load", f => {
            if (xhr.status == 200) {
                if (xhr.response == "false") {

                } else {
                    select.querySelectorAll("option").forEach(e => {
                        e.remove();
                    })
                    let datos = JSON.parse(xhr.responseText);
                    for (let i = 0; datos.length > i; i++) {
                        let option = document.createElement("option");
                        option.value = datos[i].institucionesId;
                        option.textContent = datos[i].nombre;
                        select.append(option);


                    }
                    console.log(datos)
                }


            }
        })
        let Municipios = e.target.value;
        console.log(Municipios)

        xhr.send();
    }
})




function ValidarFecha(fecha) {
    const fechaNacimiento = new Date(fecha.value);
    const fechaActual = new Date();
    const edadMinima = 3;
   
    let spanFecha = document.getElementById("spanFechaNacimiento");
    if (isNaN(fechaNacimiento) || fechaActual.getFullYear() == fechaNacimiento.getFullYear()) {
        spanFecha.textContent = "Por favor coloque una fecha coherente o valida";
        console.log(fecha.value);
        return false;
    }
    let validarEdadMinima = fechaActual.getFullYear() - fechaNacimiento.getFullYear();
    if (validarEdadMinima < 18) {
        spanFecha.textContent = "la edad minima es 5"
        return false;
    }
    return true;
}