document.getElementById("register-button").addEventListener("click", register);
function register(){
    //get all values
    var username = document.getElementById("Username").value;
    var email = document.getElementById("Email").value;
    var password = document.getElementById("Password").value;

    //validations
    if(username.length === 0 || email.length === 0 || password.length === 0)
    {
        swal("Register Error", "You left field/s empty", "error");
    }

    //call api
    fetch('https://localhost:44332/api/Login/register',{
        method:'POST',
        dataType: 'JSON',
        headers:{
            'Content-Type':'application/json'
        },
        body: JSON.stringify({
            "userName":username,
            "email":email,
            "password":password
        })
    }).then(res=>{
        console.log(res);
    })
}