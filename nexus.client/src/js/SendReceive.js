import { getBuffer } from 'azure-x-lib'
import { encode } from "uint8-to-base64";

export async function SendMsg() {
    // var message = "Yo, Azure";

    // const response = await fetch(`user/send/x/${message}`, {
    //   method: "POST",
    // });
  
    // const data = await response.json();
  
    // console.log(data);

    
}

async function fileInputChangeHadler(e){
  const filedata = new Blob([e.target.files[0]]);
  if (filedata.size > 9) {
    var promise = new Promise(getBuffer(filedata));
    await promise
      .then(async function (data) {
        const b64encoded = encode(data);
        console.log(b64encoded);   
      });
  }
}

export async function SetFilesInput(){
    const fileInput = document.getElementById('file-n-input');

    fileInput.addEventListener('change',fileInputChangeHadler);


}

