CREATE TABLE IF NOT EXISTS author
(
    id BIGSERIAL PRIMARY KEY,
    external_id UUID NOT NULL,
    slug VARCHAR(80),
    full_name VARCHAR(100) NOT NULL,
    bio VARCHAR(500),
    created_by BIGINT,
    updated_by BIGINT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(external_id),
    UNIQUE(slug)
);
