import React from 'react';
import ReactModal from 'react-modal-resizable-draggable';

class Resizablevideo extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            modalIsOpen: true
        };
    }
  
    openModal() {
        this.setState({modalIsOpen: true});
    }
    closeModal() {
        this.setState({modalIsOpen: false});
    }

  render() {
      console.log('Props: ', this.props);
        return (
            <div>
                <span className="close-btn" onClick={this.closeModal.bind(this)}>X</span>
                <ReactModal 
                    initWidth={800} 
                    initHeight={400} 
                    onFocus={() => console.log("Modal is clicked")}
                    onRequestClose={this.closeModal} 
                    isOpen={this.state.modalIsOpen}>
                    <div>
                        <iframe
                        // src='https://www.youtube.com/embed/eJL7q3uVaDw'
                        src={`https://www.youtube.com/embed/${this.props.videoId}`}
                        frameBorder="0"
                        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                        />
                    </div>
                </ReactModal>
            </div>
        );
  }
}

export default Resizablevideo;