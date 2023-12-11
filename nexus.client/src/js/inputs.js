import { email_pattern, user_pattern, password_pattern } from "./patterns";

function ComparePasswords(){
    var passwords = document.querySelectorAll('input[type="password"]');
    if(passwords[0].value === passwords[1].value){
        passwords[0].style.borderColor = "var(--theme-color-border)";
        passwords[1].style.borderColor = "var(--theme-color-border)";
    }
    else{
        passwords[0].style.borderColor = "red";
        passwords[1].style.borderColor = "red";
    }
    return passwords[0].value === passwords[1].value;
}

export function LabelFocus(inputs) {
  for (var i = 0; i < inputs.length; i++) {
    if (inputs[i].type === "text" && inputs[i].id === "UserName-X") {
      inputs[i].addEventListener("focus", (eu) => {
        var x = document.querySelector(`label[for="${eu.target.id}"]`);
        x.classList.add("input-x-label-focus");
        eu.target.style.borderColor = "var(--theme-color-border-focus-i)";
      });

      inputs[i].addEventListener("blur", (eu) => {
        if (eu.target.value.length <= 0) {
          var x = document.querySelector(`label[for="${eu.target.id}"]`);
          x.classList.remove("input-x-label-focus");
         
        }
        if (!user_pattern.test(eu.target.value) && eu.target.value.length > 0) {
          eu.target.style.borderColor = "red";
        } else {
            if(eu.target.style.borderColor !== "var(--theme-color-border)"){
                eu.target.style.borderColor = "var(--theme-color-border)";
            }
        }
      });
    }

    if (inputs[i].type === "text" && inputs[i].id === "UserEmail-X") {


        inputs[i].style.textTransform = "lowercase";

        

      inputs[i].addEventListener("focus", (eu) => {
        var x = document.querySelector(`label[for="${eu.target.id}"]`);
        x.classList.add("input-x-label-focus");
        eu.target.style.borderColor = "var(--theme-color-border-focus-i)";
      });

      inputs[i].addEventListener("blur", (eu) => {
        if (eu.target.value.length <= 0) {
          var x = document.querySelector(`label[for="${eu.target.id}"]`);
          x.classList.remove("input-x-label-focus");
        }
        if (
          !email_pattern.test(eu.target.value) &&
          eu.target.value.length > 0
        ) {
          eu.target.style.borderColor = "red";
        } else {
            if(eu.target.style.borderColor !== "var(--theme-color-border)"){
                eu.target.style.borderColor = "var(--theme-color-border)";
            }
        }
      });
    }



    if (inputs[i].type === "password") {
      inputs[i].addEventListener("focus", (eu) => {
        var x = document.querySelector(`label[for="${eu.target.id}"]`);
        x.classList.add("input-x-label-focus");
        eu.target.style.borderColor = "var(--theme-color-border-focus-i)";
      });

      inputs[i].addEventListener("blur", (eu) => {
        if (eu.target.value.length <= 0) {
          var x = document.querySelector(`label[for="${eu.target.id}"]`);
          x.classList.remove("input-x-label-focus");
        }

        if (
            (!password_pattern.test(eu.target.value) &&
            eu.target.value.length > 0) || !ComparePasswords()
          ) {
            eu.target.style.borderColor = "red";
          } else {
            if(eu.target.style.borderColor !== "var(--theme-color-border)"){
                eu.target.style.borderColor = "var(--theme-color-border)";
            }
          }
      });
      
    }
  }
}


export function LabelFocusSmall(inputs) {
  for (var i = 0; i < inputs.length; i++) {
    if (inputs[i].type === "text" && inputs[i].id === "UserName-X") {
      inputs[i].addEventListener("focus", (eu) => {
        var x = document.querySelector(`label[for="${eu.target.id}"]`);
        x.classList.add("input-x-label-focus");
        eu.target.style.borderColor = "var(--theme-color-border-focus-i)";
      });

      inputs[i].addEventListener("blur", (eu) => {
        if (eu.target.value.length <= 0) {
          var x = document.querySelector(`label[for="${eu.target.id}"]`);
          x.classList.remove("input-x-label-focus");
         
        }
        if (!user_pattern.test(eu.target.value) && eu.target.value.length > 0) {
          eu.target.style.borderColor = "red";
        } else {
            if(eu.target.style.borderColor !== "var(--theme-color-border)"){
                eu.target.style.borderColor = "var(--theme-color-border)";
            }
        }
      });
    }


    if (inputs[i].type === "password") {
      inputs[i].addEventListener("focus", (eu) => {
        var x = document.querySelector(`label[for="${eu.target.id}"]`);
        x.classList.add("input-x-label-focus");
        eu.target.style.borderColor = "var(--theme-color-border-focus-i)";
      });

      inputs[i].addEventListener("blur", (eu) => {
        if (eu.target.value.length <= 0) {
          var x = document.querySelector(`label[for="${eu.target.id}"]`);
          x.classList.remove("input-x-label-focus");
        }

        if (
            !password_pattern.test(eu.target.value) &&
            eu.target.value.length > 0
          ) {
            eu.target.style.borderColor = "red";
          } else {
            if(eu.target.style.borderColor !== "var(--theme-color-border)"){
                eu.target.style.borderColor = "var(--theme-color-border)";
            }
          }
      });
      
    }
  }
}

export function TestPatterns()
{
  return ComparePasswords() && password_pattern.test(document.getElementById('Password-X').value) &&
   password_pattern.test(document.getElementById('Password-R').value) && email_pattern.test(document.getElementById('UserEmail-X').value)
    && user_pattern.test(document.getElementById('UserName-X').value);
}