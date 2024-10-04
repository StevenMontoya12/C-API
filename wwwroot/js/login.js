document.getElementById('loginForm').addEventListener('submit', async function (e) {
    e.preventDefault(); // Evitar el envío del formulario

    const requestData = {
        CorreoElectronico: document.getElementById('correoElectronico').value,
        Password: document.getElementById('password').value
    };

    const response = await fetch('api/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(requestData)
    });

    if (response.ok) {
        const result = await response.json();
        console.log('Respuesta del servidor:', result); // Verifica los datos devueltos

        // Verifica el campo "role" en minúsculas
        if (result.role === "Empleado") {
            // Redirigir a la vista de empleado
            window.location.href = "vista/empleado.html"; // Asegúrate de que la ruta existe
        } else if (result.role === "Cliente") {
            // Redirigir a la vista de cliente
            window.location.href = "vista/cliente.html"; // Asegúrate de que la ruta existe
        } else {
            console.log("Rol desconocido:", result.role); // Manejar el caso de un rol no esperado
        }
    } else {
        // Manejar error de inicio de sesión
        alert("Credenciales inválidas");
    }
});
