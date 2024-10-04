document.addEventListener('DOMContentLoaded', async function () {
    // Obtener el empleadoID desde localStorage
    const empleadoID = localStorage.getItem('empleadoID');
    console.log("EmpleadoID desde localStorage:", empleadoID);

    if (!empleadoID) {
        console.log("No se encontró el empleadoID en localStorage. Redirigiendo a index.html...");
        window.location.href = "index.html"; // Redirigir si no hay empleadoID en localStorage
        return;
    }

    try {
        // Hacer una solicitud GET a la API para obtener los datos del empleado
        const response = await fetch(`http://localhost:5085/api/empleado/${empleadoID}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        console.log("Estado de la respuesta de la API:", response.status); // Verificar el estado de la respuesta
        if (response.ok) {
            const empleado = await response.json(); // Obtener los datos del empleado en formato JSON
            console.log("Datos del empleado:", empleado);

            // Rellenar los campos con la información del empleado
            document.getElementById('nombreEmpleado').textContent = empleado.nombre;
            document.getElementById('empleadoID').textContent = empleado.empleadoID;
            document.getElementById('nombreCompleto').textContent = `${empleado.nombre} ${empleado.apellido}`;
            document.getElementById('correoEmpleado').textContent = empleado.correoElectronico;
        } else {
            console.error('Error al obtener los datos del empleado:', response.statusText);
            alert("Error al cargar los datos del empleado.");
        }
    } catch (error) {
        console.error('Error de red:', error);
        alert("Error al conectarse con el servidor.");
        window.location.href = "index.html"; // Redirigir en caso de error
    }
});
