export async function SendMsg() {
    var message = "Yo, Azure";
    const response = await fetch(`user/send/x/${message}`, {
      method: "POST",
    });
  
    const data = await response.json();
  
    console.log(data);
  }