<template>
    <div>
    <table class="table">
        <thead>
            <tr class="tr">
                <th class="th">Person</th>
                <th class="th">Message</th>
                <th class="th">Time Sent</th>
            </tr>
        </thead>
        <tbody>
            <tr class="tr">
                <td class="td">Mukund</td>
                <td class="td">This is the first message!</td>
                <td class="td"></td>
            </tr>
        </tbody>
    </table> 
    </div>
    
    <div class="centered button-centered">
        <input type="text" class="styled-input" placeholder="Your Name" />
        <input type="text" class="styled-input" placeholder="My Name" />
        <br>
        <input v-model="message" placeholder="Enter your message" />
        <button class="button-30" @click="sendMessage">Send!</button>
    </div>
</template>


<script>
import { defineComponent, ref } from 'vue';

export default defineComponent({
  name: 'MessageSender',
  setup() {
    const message = ref('');

    const sendMessage = async () => {
      if (!message.value) {
        alert('Please enter a message.');
        return;
      }

      try {
        const response = await fetch('http://localhost:5121/api/message/broadcast', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ message: message.value }),
        });

        if (!response.ok) {
          throw new Error('Network response was not ok');
        }

        const data = await response.json();
        console.log("received response");
        console.log(data); // Handle the response if needed
        //alert('Message sent successfully!');
      } catch (error) {
        console.error('Error:', error);
        //alert('Failed to send message.');
      }
    };

    return {
      message,
      sendMessage,
    };
  },
});
</script>

<style>

    body {
        font-family: Arial, sans-serif;
    }
    
    .centered {
        text-align: center;
        padding: 10px;
        margin: 10px;
    }

    .table {
        width: 50%;
        border-collapse: collapse;
        margin: 10px auto;
    }

    .th, .td {
        border: 1px solid #331c88;
        padding: 10px;
        text-align: center;
    }

    .th {
        background-color: #331c88;
        color: white;
    }

    .td {
        background-color: #e6a3f0;
    }

    .styled-input {
        width: 20%; /* Full width */
        padding: 6px 10px 10px 10px; /* Spacing inside the textbox */
        margin: 5px;
        border: 1px solid #ccc; /* Light gray border */
        border-radius: 4px; /* Rounded corners */
        font-size: 16px; /* Font size */
        color: #333; /* Text color */
    }
    
    .styled-input:focus {
        border-color: #3498db; /* Change border color on focus */
        outline: none; /* Remove default outline */
        box-shadow: 0 0 5px rgba(52, 152, 219, 0.5); /* Subtle shadow effect */
    }

    /* CSS */
    .button-30 {
        align-items: center;
        appearance: none;
        background-color: #FCFCFD;
        border-radius: 4px;
        border-width: 0;
        box-shadow: rgba(45, 35, 66, 0.4) 0 2px 4px,rgba(45, 35, 66, 0.3) 0 7px 13px -3px,#D6D6E7 0 -3px 0 inset;
        box-sizing: border-box;
        color: #36395A;
        cursor: pointer;
        display: inline-flex;
        height: 30px;
        justify-content: center;
        line-height: 1;
        list-style: none;
        overflow: hidden;
        position: relative;
        text-align: left;
        text-decoration: none;
        transition: box-shadow .15s,transform .15s;
        user-select: none;
        -webkit-user-select: none;
        touch-action: manipulation;
        white-space: nowrap;
        will-change: box-shadow,transform;
        font-size: 16px;
    }

    .button-30:focus {
        box-shadow: #D6D6E7 0 0 0 1.5px inset, rgba(45, 35, 66, 0.4) 0 2px 4px, rgba(45, 35, 66, 0.3) 0 7px 13px -3px, #D6D6E7 0 -3px 0 inset;
    }

    .button-30:hover {
        box-shadow: rgba(45, 35, 66, 0.4) 0 4px 8px, rgba(45, 35, 66, 0.3) 0 7px 13px -3px, #D6D6E7 0 -3px 0 inset;
        transform: translateY(-2px);
    }

    .button-30:active {
        box-shadow: #D6D6E7 0 3px 7px inset;
        transform: translateY(2px);
    }
</style>