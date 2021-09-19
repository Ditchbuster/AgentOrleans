import React, { Component } from 'react';
import Button  from '@material-ui/core/Button';
import { List, ListItem, ListItemIcon, ListItemText } from '@material-ui/core'
import { DataGrid } from '@material-ui/lab';

export class Agent extends Component {
  static displayName = Agent.name;

  static options = [
    {
      label: "Apple",
      value: "apple",
    },
    {
      label: "Mango",
      value: "mango",
    },
    {
      label: "Banana",
      value: "banana",
    },
    {
      label: "Pineapple",
      value: "pineapple",
    },
  ];

  constructor(props) {
    super(props);
    this.state = { date: new Date(), index: null, loading: true, data: []};
  }

  componentDidMount() {
    this.timerID = setInterval(() => this.tick(), 1000);
    //this.populateData();
  }

  componentWillUnmount() {
    clearInterval(this.timerID);
  }

  tick() {
    this.setState({ date: new Date() });
  }

  handleListItemClick(event, i) {
    this.setState({ index: i });
  }

  listItemRender(index, name) {
    return (
      <ListItem button selected={this.state.index === index} onClick={(event) => this.handleListItemClick(event, index)}>
            <ListItemIcon>
              {this.state.index}
            </ListItemIcon>
            <ListItemText primary={name} />
      </ListItem>
    )
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : <List component="nav">{this.state.data.map((option,index) => this.listItemRender(index,option))}</List>
    
    return (
      <div>
        <h1>Agent</h1>

        <p>{ this.state.date.toLocaleTimeString()}</p>
        <button className="btn btn-primary" onClick={() => this.populateData()}>Test</button>
        <div className="select-container">
          <select>
            {Agent.options.map((option) => (<option value={option.value}>{option.label}</option>))}
          </select>
        </div>
        <Button variant="outlined" color="primary" onClick={()=> this.setState({index: null})}>Hello World</Button>
        {contents}

      </div>
    );
  }

  async populateData() {
    //const response = await fetch('weatherforecast');
    //const data = await response.json();
    this.setState({ data: ['test','foo','bar'], loading: false });
  }
  
}
