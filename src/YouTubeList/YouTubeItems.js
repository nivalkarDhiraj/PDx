
import React from 'react';
import { Row, Card, Col } from 'react-bootstrap'
import './YouTubeItems'
import Moment from 'react-moment';

const YouTubeItem = ({ video, handleVideoSelect }) => {
  return (
    <>
      {/* Create Card for to show image, title and publish date */}
      <span className="d-inline-block">
        <Card className="m-2border-0 shadow" onClick={() => handleVideoSelect(video)}  >
          <Row>
            <Col>
              <Card.Img src={video.snippet.thumbnails.medium.url} alt={video.snippet.description} />
            </Col>
            <Col>
              <Card.Body>
                <Card.Title >  <div data-toggle="tooltip" data-placement="right" title={video.snippet.description}>
                  {video.snippet.title}
                </div></Card.Title>
                <Card.Text >
                  <p><strong> Published By</strong> <Moment format="MMM-DD-YYYY">{video.snippet.publishTime}</Moment></p>
                </Card.Text>
              </Card.Body>
            </Col>
          </Row>
        </Card>
      </span>
    </>
  );
}

export default YouTubeItem;