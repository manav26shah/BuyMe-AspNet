var form=document.getElementById("login-button");
form.addEventListener("click",Login);

function Login(){
    var email=document.getElementById("email").value;
    var password=document.getElementById("password").value;
    fetch('https://localhost:44332/api/Login',{
        method:'POST',
        headers:{
            'Content-Type':'application/json'
        },
        body: JSON.stringify({
            "email":email,
            "password":password
        })
    }).then(res=>{
        console.log(res);
    })
}