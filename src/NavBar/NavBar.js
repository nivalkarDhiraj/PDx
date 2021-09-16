import React, { Component } from 'react';
import { Nav, Navbar, Container, Form, FormControl, Button } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

class NavBar extends Component {

  //text on changes in search text set searchtext 
  handleChange = (event) => {
    this.setState({
      searchtext: event.target.value
    });
  };

  //search submit button call
  handleSubmit = event => {
    event.preventDefault();
    this.props.handleFormSubmit(this.state.searchtext);
  }
  //search covid 19 videos
  searchCovid = event => {
    event.preventDefault();
    this.props.handleFormSubmit('COVID-19');
  }
  //exclude COVID-19 videos
  searchECovid = event => {
    event.preventDefault();
    this.props.handleFormSubmit('-"COVID-19"');
  }
  render() {
    return (
      <div>
        <>
          <Navbar bg="primary" variant="dark">
            <Container>
              <Navbar.Brand>HCA Library</Navbar.Brand>
              <Nav className="me-auto">
                <Nav.Link onClick={this.searchCovid}>COVID</Nav.Link>
                <Nav.Link onClick={this.searchECovid}>All Videos</Nav.Link>
              </Nav>
              <Form className="d-flex" onSubmit={this.handleSubmit} >
                <FormControl onChange={this.handleChange}
                  type="search"
                  placeholder="Search"
                  className="mr-2"
                  aria-label="Search"
                />
                <Button onClick={this.handleSubmit} class="btn btn-outline-light" variant="btn btn-outline-light">Search</Button>
              </Form>
            </Container>
          </Navbar>
        </>
      </div>
    );
  }
}

export default NavBar;