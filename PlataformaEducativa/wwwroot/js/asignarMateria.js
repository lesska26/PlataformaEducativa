class AsginarMateria {
    asignar(e) {
        e.preventDefault();
        let materia = [];
        var div = document.querySelector(".cursosDiv").querySelectorAll(".form-check");
        div.forEach(f => {
            let checkbox = f.querySelector("input");
            if (checkbox.checked && checkbox.disabled==false)
            {
                materia.push(checkbox.value);
                console.log(checkbox.value)
            }
        })
        if (materia.length > 0) {
            let xhr = new XMLHttpRequest();
            xhr.open("post", "/Usuario/AsignarMateria");
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.addEventListener("load", e => {
                if (xhr.status == 200) {
                    window.location.href = "/Usuario/VerProfesor";
                }
            })
            let UsuarioId = document.querySelector("#ProfesorId").value;
            let datos = {
                UsuarioId,
                materia
            }
            let Dato = JSON.stringify(datos);
            console.log(datos);
            xhr.send(Dato)
        } else {
            alert("tiene que elegir una materia");
        }
        
    }
}
document.querySelector("#asignar").addEventListener("click", new AsginarMateria().asignar)