import React, {useState} from "react";
import axios from "axios";
import gamesApi from './gamesApi'
import auth from "../Models/Client/Auth";
import Token from "../Models/Client/Token";
import Auth from "../Models/Client/Auth";
import PropTypes from "prop-types";
// import {
//     MDBBtn,
//     MDBContainer,
//     MDBRow,
//     MDBCol,
//     MDBCard,
//     MDBCardBody,
//     MDBInput,
//     MDBIcon
// } from 'mdb-react-ui-kit';
import '../index.css';





    export default function AuthPage({ setToken }) {
        //const [auth, setDate] = useState();
             const [login, setUserName] = useState();
            const [password, setPassword] = useState();
        const handleSubmit =  async e => {
            e.preventDefault();
            console.log(login, password)
            const token = await gamesApi.authorization({
                login, password
            });
            console.log(token);
            setToken(token);
            window.location.replace('http://localhost:3000/main');
        }

        // return(
        //     <div className="login-wrapper">
        //         <h1>Please Log In</h1>
        //         <form onSubmit={handleSubmit}>
        //             <label>
        //                 <p>Username</p>
        //                 <input type="text" onChange={e => setUserName(e.target.value)} />
        //             </label>
        //             <label>
        //                 <p>Password</p>
        //                 <input type="password" onChange={e => setPassword(e.target.value)} />
        //             </label>
        //             <div>
        //                 <button type="submit">Submit</button>
        //             </div>
        //         </form>
        //     </div>
        // )

        // return (
        //     <MDBContainer fluid>
        //
        //         <MDBRow className='d-flex justify-content-center align-items-center h-100'>
        //             <MDBCol col='12'>
        //
        //                 <MDBCard className='bg-dark text-white my-5 mx-auto' style={{borderRadius: '1rem', maxWidth: '400px'}}>
        //                     <MDBCardBody className='p-5 d-flex flex-column align-items-center mx-auto w-100'>
        //
        //                         <h2 className="fw-bold mb-2 text-uppercase">Login</h2>
        //                         <p className="text-white-50 mb-5">Please enter your login and password!</p>
        //
        //                         <MDBInput wrapperClass='mb-4 mx-5 w-100' labelClass='text-white' label='Email address' id='formControlLg' type='email' size="lg"/>
        //                         <MDBInput wrapperClass='mb-4 mx-5 w-100' labelClass='text-white' label='Password' id='formControlLg' type='password' size="lg"/>
        //
        //                         <p className="small mb-3 pb-lg-2"><a className="text-white-50" href="#!">Forgot password?</a></p>
        //                         <MDBBtn outline className='mx-2 px-5' color='white' size='lg'>
        //                             Login
        //                         </MDBBtn>
        //
        //                         <div className='d-flex flex-row mt-3 mb-5'>
        //                             <MDBBtn tag='a' color='none' className='m-3' style={{ color: 'white' }}>
        //                                 <MDBIcon fab icon='facebook-f' size="lg"/>
        //                             </MDBBtn>
        //
        //                             <MDBBtn tag='a' color='none' className='m-3' style={{ color: 'white' }}>
        //                                 <MDBIcon fab icon='twitter' size="lg"/>
        //                             </MDBBtn>
        //
        //                             <MDBBtn tag='a' color='none' className='m-3' style={{ color: 'white' }}>
        //                                 <MDBIcon fab icon='google' size="lg"/>
        //                             </MDBBtn>
        //                         </div>
        //
        //                         <div>
        //                             <p className="mb-0">Don't have an account? <a href="#!" className="text-white-50 fw-bold">Sign Up</a></p>
        //
        //                         </div>
        //                     </MDBCardBody>
        //                 </MDBCard>
        //
        //             </MDBCol>
        //         </MDBRow>
        //
        //     </MDBContainer>
        // );
        return (
            <form>
                <h3>Sign In</h3>
                <div className="mb-3">
                    <label>Email address</label>
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Enter email"
                    />
                </div>
                <div className="mb-3">
                    <label>Password</label>
                    <input
                        type="password"
                        className="form-control"
                        placeholder="Enter password"
                    />
                </div>
                <div className="mb-3">
                    <div className="custom-control custom-checkbox">
                        <input
                            type="checkbox"
                            className="custom-control-input"
                            id="customCheck1"
                        />
                        <label className="custom-control-label" htmlFor="customCheck1">
                            Remember me
                        </label>
                    </div>
                </div>
                <div className="d-grid">
                    <button type="submit" className="btn btn-primary">
                        Submit
                    </button>
                </div>
                <p className="forgot-password text-right">
                    Forgot <a href="#">password?</a>
                </p>
            </form>
        )

    }

    AuthPage.propTypes = {
        setToken: PropTypes.func.isRequired
    };
