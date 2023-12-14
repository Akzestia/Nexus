import { getBuffer } from 'azure-x-lib'
import { encode } from "uint8-to-base64";

export async function SendMsg(SenderName, ReceiverName) {

    if(ReceiverName.length < 2 || document.getElementById('message-n-input').value.trim().length <= 0){
      return;
    }
    else{
      alert(ReceiverName);
    }

    const _sender = SenderName + '';
    const _receiver = ReceiverName + '';
    console.log(_sender + '   ' + _receiver);
    const message = {
      TextContent: document.getElementById('message-n-input').value,
      ByteContent: null,
      Sender: _sender + "",
      Receiver: _receiver + ""
    };

    const response = await fetch('server/send',{
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(message),
    });

    const data = await response.json();

    console.log(data);

    document.getElementById('message-n-input').value = '';
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

