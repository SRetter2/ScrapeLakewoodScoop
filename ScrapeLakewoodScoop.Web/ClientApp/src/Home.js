import React from 'react';
import axios from 'axios';

class Home extends React.Component {
    state = {
        scoops:[]
    }
    componentDidMount = async () => {
        const { data } = await axios.get('api/scoop/getall');
        this.setState({ scoops: data });
    }

    render()
    {
        return (
            <div className='container' style={{ marginTop: 60 }}>
                <div className='row col-md-6'>
                    {this.state.scoops.map(scoop =>
                        <div className='well'>
                            <h2>
                                <a href={scoop.url}>{scoop.title}</a>
                                </h2>
                            <img src={scoop.imageUrl} style={{ width: 300 }}/>
                            <h4>{scoop.blurb}</h4>
                            <span>{scoop.commentCount}</span>
                        </div>
                        )}
                    </div>
                </div>
            );
    }

}

export default Home;