import { TestPatterns } from './inputs';

export async function SignUpUser() {

//   alert(TestPatterns());
//   return;
  var userName = "Alex";
  var userEmail = "Alex@gmail.com";

  var user_data = {
        userName: document.getElementById('UserName-X').value,
        userEmail: document.getElementById('UserEmail-X').value,
        userPassword: document.getElementById('Password-X').value,
  };

  let timer;
  let turn = 0;
 
  var darkmode = document.querySelector('.dark-x-div');
  darkmode.classList.add('active-x-dark');
  var icon = document.querySelector('.loading-x-icon');
  icon.style.transform = "rotate("+ (turn) +"deg)";
  timer = setInterval(() =>{
        icon.style.transform = "rotate("+ (turn) +"deg)";
        turn += 10;
  }, 10);

  const response = await fetch(`server/connect`, {
    method: "POST",
  });

  const data = await response.json();

  var loading_label = document.querySelector('.loading-x-label');
  
  const clickHandler = (e) => {
        e.target.classList.remove('active-x-dark');
        setTimeout(function() {
            loading_label.innerHTML = "Connecting to server";
        }, 750);
        e.target.removeEventListener('click', clickHandler);
    };

  if(!data){
        loading_label.innerHTML = "Connection Failed :(";
        clearInterval(timer);
        darkmode.addEventListener('click', clickHandler);
  }
  else{
        
        loading_label.innerHTML = "Redirecting to Nexus ";
        setTimeout(function() {
                window.location = "/nexus";
        }, 1500);
        setTimeout(function() {
                loading_label.innerHTML = "Connecting to server";
                clearInterval(timer);
          }, 5000);
         
  }
}

export async  function LoginUser(){


        var user_data = {
                userName: document.getElementById('UserName-X').value,
                userPassword: document.getElementById('Password-X').value,
        };


        const response = await fetch(`server/login`, {
                method: "POST",
                headers: {
                        "Content-Type": "application/json",
                },
                body: JSON.stringify(user_data)
        });

        const data = await response.json();

        console.log('response ' + data);


        console.log('dispose');
        // const responsex = await fetch(`server/send`, {
        //         method: "POST",
        //         headers: {
        //                 'Content-Type': 'application/json',
        //                 'Accept': 'application/json'
        //         }
        // });


        // const datax = await responsex.json();

        // console.log(datax);
        // setTimeout(async () =>{
               

        //         console.log(await response.json());
        // }, 1)
}