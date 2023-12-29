document.getElementById('formulario').addEventListener('submit', function (event) {
    var startTime = document.getElementById('meeting-start').value;
    var endTime = document.getElementById('meeting-end').value;
    var dato = endTime.split(":");
    var dato2 = startTime.split();
    let result = parseInt(dato[0]) - parseInt(dato2[0]);

    console.log(startTime)
    console.log(endTime);
    if (startTime >= endTime) {
        alert('La hora de terminación de la clase no puede ser menor o igual a la hora de inicio.');
        event.preventDefault();
    } else {
        event.preventDefault();
        let xhr = new XMLHttpRequest();
        xhr.open("POST", "/Hora/Create");
        xhr.setRequestHeader("Content-Type", "application/json");
        let datos = {
            Horas_Iniciar: startTime,
            Horas_Final: endTime,
            CatidadHora: result
        }
        let json = JSON.stringify(datos);
        xhr.send(json);
        alert('registro completad, ' + 'Horas de clases son: ' + result)


    }
});
