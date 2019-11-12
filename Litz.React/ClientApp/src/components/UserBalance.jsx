import React, { Component } from 'react'
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Counter';

const UserBalance = props => (
    <div>
        <p>Bom dia, teste</p>

        <h3 onClick={props.increment} class="text-center visible-lg-inline">R$</h3> <h1 class="bold visible-lg-inline"> Porra</h1>
    </div>
)


export default connect(
    state => state.counter,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(UserBalance);