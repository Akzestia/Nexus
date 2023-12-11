import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import LoginX from './Login.jsx'
import SignUp from './SignUp.jsx'
import { Auth } from './IsAuth.jsx'
import Nexus from './Nexus.jsx'
import './js/styles.js'


ReactDOM.createRoot(document.getElementById('root')).render(
    <BrowserRouter>
            <Routes>
                <Route path="/" element={<Auth></Auth>}></Route>

                <Route path="/login" element={<LoginX></LoginX> }></Route>

                <Route path="/signup" element={<SignUp></SignUp> }></Route>

                <Route path="/nexus" element={<Nexus></Nexus>}></Route>
            </Routes>
    </BrowserRouter>
)
